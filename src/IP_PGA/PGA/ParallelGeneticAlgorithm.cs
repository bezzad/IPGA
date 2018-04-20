using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;
using System.Threading.Tasks;

namespace IP_PGA.PGA
{
    public class ParallelGeneticAlgorithm
    {
        #region Properties
        public System.Windows.Forms.PictureBox PicBox;
        public System.Windows.Forms.Label lblGeneration;
        public System.Windows.Forms.Label lblFitness;
        public System.Windows.Forms.Button btnStartStop;
        public System.Windows.Forms.Button btnPauseResume;
        public bool ParallelProcess = false;
        public bool DisplayGraphical = true;

        private int maxCPU = 1;
        private CancellationTokenSource tokenSource;
        private ProcessState _State = ProcessState.NotRunning;
        public ProcessState State
        {
            get { return _State; }
        }

        object _locker = new object();

        // create new process or for end process
        Thread ManagerThread;
        
        //
        // GA Parameters:
        //
        // Number Population
        int Npop = 100;

        // Number Keep Chromosome Size 
        int N_keep = 0;

        // Double Array Pn for save Rank
        double[] Pn;

        // Save DNA information in Chromosome Array
        Chromosome[] pop;
        #endregion

        #region Thread Invoked
        public static void UIInvoke(Control uiControl, Action action)
        {
            if (!uiControl.IsDisposed)
            {
                if (uiControl.InvokeRequired)
                {
                    uiControl.BeginInvoke(action);
                }
                else
                {
                    action();
                }
            }
        }
        // ------------------------------------------------------------
        // This delegate enables asynchronous calls for setting
        // the Text property on a Label Generation control.
        private void setGenerationText(string v)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            try
            {
                UIInvoke(this.lblGeneration, delegate()
                {
                    this.lblGeneration.Text = v;
                });
            }
            catch { }
        }
        // ------------------------------------------------------------
        // This delegate enables asynchronous calls for setting
        // the Text property on a Label Fitness control.
        private void setFitnessText(string v)
        {
            try
            {
                try
                {
                    UIInvoke(this.lblFitness, delegate()
                    {
                        this.lblFitness.Text = v;
                    });
                }
                catch { }
            }
            catch { }
        }
        // ------------------------------------------------------------
        // This delegate enables asynchronous calls for setting
        // the Image property on a PictureBox PicBox control.
        private void setPicBoxImage(Bitmap v)
        {
            try
            {
                UIInvoke(this.PicBox, delegate()
                {
                    this.PicBox.Image = v;
                });
            }
            catch { }
        }
        #endregion

        /// <summary>
        /// Constructor of PGA class
        /// </summary>
        public ParallelGeneticAlgorithm()
        {
            maxCPU = System.Environment.ProcessorCount / 2;
        }

        /// <summary>
        /// Parallel Genetic Algorithm for Image Processing
        /// </summary>
        private void PGA()
        {
            Random rand = new Random();
            
            tokenSource = new CancellationTokenSource();

            double EliteFitness = double.MaxValue;

            // Create first population by Npop = 500;
            Population();

            // Evaluate Fitness
            for (int i = 0; i < Npop; i++)
                pop[i].Calculate_Fitness();

            int count = 1; // generation counter
            do
            {
                #region Selection
                #region Bubble Sort all chromosome by fitness
                // 
                for (int i = Npop - 1; i > 0; i--)
                    for (int j = 1; j <= i; j++)
                        if (pop[j - 1].Fitness > pop[j].Fitness)
                        {
                            Chromosome ch = pop[j - 1];
                            pop[j - 1] = pop[j];
                            pop[j] = ch;
                        }
                //
                #endregion

                #region Elitism
                if (EliteFitness > pop[0].Fitness)
                {
                    EliteFitness = pop[0].Fitness;

                    if (DisplayGraphical)
                    {
                        //
                        // Set elite chromosome in PictureBox.Image
                        Bitmap displayImage = new Bitmap(Chromosome.imageWidth, Chromosome.imageHeight);
                        for (int x = 0; x < Chromosome.imageWidth; x++)
                            for (int y = 0; y < Chromosome.imageHeight; y++)
                                displayImage.SetPixel(x, y, Color.FromArgb(pop[0].imageMatrix[x, y], pop[0].imageMatrix[x, y], pop[0].imageMatrix[x, y]));

                        setPicBoxImage(displayImage);
                    }
                    //
                    //-----------------------------------------------------------------------------
                    setFitnessText(pop[0].Fitness.ToString());
                    //
                }
                //else setTimeGraph(EliteFitness, count, false); // just refresh Generation Graph's

                #endregion
                x_Rate(); // Selection any worst chromosome for clear and ...
                #endregion

                #region Reproduction
                // Definition Probability According by chromosome fitness 
                // create Pn[N_keep];
                Rank_Trim();

                if (ParallelProcess) ReproduceByParallelTask(); //parallel
                else Reproduction(rand); //series
                #endregion

                count++;
                setGenerationText(count.ToString());
                // lblGeneration.Text = count.ToString();

                Monitor.Enter(_locker);
                Monitor.Exit(_locker);
            }
            while (count < 100000 && State != ProcessState.Stopped_Aborted &&
                State != ProcessState.Finished); // && Isotropy_Evaluatuon()

            //
            // The END
            _State = ProcessState.Finished;
            Stop();
        }

        #region Generation Tools
        private void Population()
        {
            // Create first population by Npop = 500;
            pop = new Chromosome[Npop];

            pop[0] = new Chromosome();
            pop[0].clear(0); // Black Image
            pop[1] = new Chromosome();
            pop[1].clear(255); // White Image

            for (int i = 0; i < Npop; i++)
            {
                pop[i] = new Chromosome();
                pop[i].CreateBitmapRandomize();
            }
        }

        /// <summary>
        /// Find percent of All chromosome rate for delete Amiss(xRate) or Useful(Nkeep) chromosome
        /// x_Rate According by chromosome fitness Average 
        /// </summary>
        private void x_Rate()
        {
            // calculate Addition of all fitness
            double sumFitness = 0;
            for (int i = 0; i < Npop; i++)
                sumFitness += pop[i].Fitness;
            // calculate Average of All chromosome fitness 
            double aveFitness = sumFitness / Npop; //Average of all chromosome fitness
            N_keep = 0; // N_keep start at 0 till Average fitness chromosome
            for (int i = 0; i < Npop; i++)
                if (aveFitness >= pop[i].Fitness)
                {
                    N_keep++; // counter as 0 ~ fitness Average + 1
                }
            if (N_keep <= 0) N_keep = 2;
        }

        // Definition Probability According by chromosome fitness 
        private void Rank_Trim()
        {
            // First Reserve Possibility Number for every Remnant chromosome 
            // chromosome Possibility Function is:
            // (1 + N_keep - No.chromosome) / ( ∑ No.chromosome) 
            // Where as at this program No.chromosome Of Array begin as Number 0
            // There for No.chromosome in Formula = No.chromosome + 1
            // then function is: if (n == N_keep)
            // Possibility[No.chromosome] = (n - No.chromosome) / (n(n+1) / 2)
            //
            Pn = new double[N_keep]; // Create chromosome possibility Array Cell as N_keep
            double Sum = ((N_keep * (N_keep + 1)) / 2); // (∑ No.chromosome) == (n(n+1) / 2)
            Pn[0] = N_keep / Sum; // Father (Best - Elite) chromosome Possibility
            for (int i = 1; i < N_keep; i++)
            {
                // Example: if ( Pn[Elite] = 0.4  &  Pn[Elite +1] = 0.2  &  Pn[Elite +2]  = 0.1 )
                // Then Own:          0 <= R <= 0.4 ===> Select chromosome[Elite]
                //                  0.4 <  R <= 0.6 ===> Select chromosome[Elite +1] 
                //                  0.6 <  R <= 0.7 ===> Select chromosome[Elite +2]
                // etc ... 
                Pn[i] = ((N_keep - i) / Sum) + Pn[i - 1];
            }
        }

        // Return Father and Mather chromosome with Probability of chromosome fitness
        private Chromosome Rank(Random rand)
        {
            double R = rand.NextDouble();
            for (int i = 0; i < N_keep; i++)
            {
                // Example: if ( Pn[Elite] = 0.6  &  Pn[Elite+1] = 0.3  &  Pn[Elite+2]  = 0.1 )
                // Then Own:          0 <= R <= 0.6  ===> Select chromosome[Elite]
                //                  0.6 <  R <= 0.9  ===> Select chromosome[Elite +1] 
                //                  0.9 <  R <= 1    ===> Select chromosome[Elite +2]
                // 
                if (R <= Pn[i]) return pop[i];
            }
            return pop[0]; // if don't run Modality of 'for' then return Elite chromosome 
        }

        // Check the isotropy All REMNANT chromosome (N_keep)
        public bool Isotropy_Evaluatuon()
        {
            // Isotropy percent is 30% of All chromosome Fitness
            int per_Iso = Convert.ToInt32(Math.Truncate(Convert.ToDouble(30 * N_keep / 100)));
            int counter_Isotropy = 0;
            double BestFitness = pop[0].Fitness;
            //
            // i start at 1 because DNA_Array[0] is self BestFitness
            for (int i = 1; i < N_keep; i++)
                if (BestFitness >= pop[i].Fitness) counter_Isotropy++;

            // G.A Algorithm did isotropy and app Stopped
            if (counter_Isotropy >= per_Iso) return false;
            else return true; // G.A Algorithm didn't isotropy and app will continued
        }

        private void ReproduceByParallelTask()
        {
            #region Parallel Reproduct Code
            Task[] tasks = new Task[maxCPU];

            int length = (Npop - N_keep) / maxCPU;
            int divideReminder = (Npop - N_keep) % maxCPU;

            for (int proc = 0; proc < tasks.Length; proc++)
            {
                ThreadToken tt = new ThreadToken(proc,
                    length + ((proc == maxCPU - 1) ? divideReminder : 0),
                    N_keep + (proc * length));

                tasks[proc] = Task.Factory.StartNew(x =>
                {
                    // work ...
                    PReproduction(((ThreadToken)x).startIndex, ((ThreadToken)x).length, ((ThreadToken)x).rand);

                }, tt, tokenSource.Token);// TaskCreationOptions.AttachedToParent);
            }

            // When user code that is running in a task creates a task with the AttachedToParent option, 
            // the new task is known as a child task of the originating task, 
            // which is known as the parent task. 
            // You can use the AttachedToParent option to express structured task parallelism,
            // because the parent task implicitly waits for all child tasks to complete. 
            // The following example shows a task that creates one child task:
            Task.WaitAll(tasks);

            // or

            //Block until all tasks complete.
            //Parent.Wait(); // when all task are into a parent task
            #endregion
        }

        /// <summary>
        /// Series Create New chromosome with Father & Mather Chromosome Instead of deleted chromosomes
        /// </summary>
        /// <param name="rand"></param>
        public void Reproduction(Random rand) // Series 
        {
            for (int i = N_keep; i < Npop; i++)
            {
                //
                // for send and check Father & Mather chromosome
                Chromosome Rank_Father, Rank_Mather, child;

                // have a problem (maybe Rank_1() == Rank_2()) then Father == Mather
                // Solve Problem by Loop checker
                do
                {
                    Rank_Father = Rank(rand);
                    Rank_Mather = Rank(rand);
                }
                while (Rank_Father == Rank_Mather);
                //
                // Crossover
                child = Rank_Father.crossover(Rank_Mather, rand);
                //
                //  run Mutation
                //
                child.mutation(rand);
                //
                // calculate children chromosome fitness
                //
                child.Calculate_Fitness();

                Interlocked.Exchange(ref pop[i], child); // atomic operation between multiple Thread shared
            }
        }
        /// <summary>
        /// Parallel Create New chromosome with Father & Mather Chromosome Instead of deleted chromosomes
        /// </summary>
        /// <param name="rand"></param>
        /// <returns></returns>
        public void PReproduction(int startIndex, int length, Random rand) // Parallel 
        {
            for (int i = startIndex; i < (startIndex + length) && i < Npop; i++)
            {
                //
                // for send and check Father & Mather chromosome
                Chromosome Rank_Father, Rank_Mather, child;

                // have a problem (maybe Rank_1() == Rank_2()) then Father == Mather
                // Solve Problem by Loop checker
                do
                {
                    Rank_Father = Rank(rand);
                    Rank_Mather = Rank(rand);
                }
                while (Rank_Father == Rank_Mather);
                //
                // Crossover
                child = Rank_Father.crossover(Rank_Mather, rand);
                //
                //  run Mutation
                //
                child.mutation(rand);
                //
                // calculate children chromosome fitness
                //
                child.Calculate_Fitness();

                Interlocked.Exchange(ref pop[i], child); // atomic operation between multiple Thread shared
            }
        }
        #endregion

        public void Start()
        {
            _State = ProcessState.Running;
            ManagerThread = new Thread(new ThreadStart(PGA));
            ManagerThread.Start();
        }
        public void Stop()
        {
            _State = ProcessState.Stopped_Aborted;
            //tokenSource.Cancel();
            if (State == ProcessState.Paused)
                Monitor.Exit(_locker);
        }
        public void Pause()
        {
            _State = ProcessState.Paused;
            Monitor.Enter(_locker);
        }
        public void Resume()
        {
            _State = ProcessState.Running;
            Monitor.Exit(_locker);
        }
    }
    public struct ThreadToken
    {
        public ThreadToken(int Thread_No, int _length, int _startIndex)
        {
            No = Thread_No;
            length = _length;
            startIndex = _startIndex;
            rand = new Random();
        }
        public int No;
        public int length;
        public int startIndex;
        public Random rand;
    };
    public enum ProcessState
    {
        NotRunning,
        Running,
        Stopped_Aborted,
        Finished,
        Paused
    }
}
