using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CpuSchedulingWinForms
{
    public class ProcessInfo
    {
        public int Id { get; set; }
        public double BurstTime { get; set; }
        public double RemainingTime { get; set; }
        public double WaitingTime { get; set; }
        public double TurnaroundTime { get; set; }
        public double StartTime { get; set; } = -1; 
        public double FinishTime { get; set; }
        public bool IsCompleted { get; set; } = false;
    }

    public static class Algorithms
    {
        public static void fcfsAlgorithm(string userInput)
        {
            int np = Convert.ToInt16(userInput);
            int npX2 = np * 2;

            double[] bp = new double[np];
            double[] wtp = new double[np];
            string[] output1 = new string[npX2];
            double twt = 0.0, awt; 
            int num;

            DialogResult result = MessageBox.Show("First Come First Serve Scheduling ", "", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (result == DialogResult.Yes)
            {
                for (num = 0; num <= np - 1; num++)
                {
                    //MessageBox.Show("Enter Burst time for P" + (num + 1) + ":", "Burst time for Process", MessageBoxButtons.OK, MessageBoxIcon.Question);
                    //Console.WriteLine("\nEnter Burst time for P" + (num + 1) + ":");

                    string input =
                    Microsoft.VisualBasic.Interaction.InputBox("Enter Burst time: ",
                                                       "Burst time for P" + (num + 1),
                                                       "",
                                                       -1, -1);

                    bp[num] = Convert.ToInt64(input);

                    //var input = Console.ReadLine();
                    //bp[num] = Convert.ToInt32(input);
                }

                for (num = 0; num <= np - 1; num++)
                {
                    if (num == 0)
                    {
                        wtp[num] = 0;
                    }
                    else
                    {
                        wtp[num] = wtp[num - 1] + bp[num - 1];
                        MessageBox.Show("Waiting time for P" + (num + 1) + " = " + wtp[num], "Job Queue", MessageBoxButtons.OK, MessageBoxIcon.None);
                    }
                }
                for (num = 0; num <= np - 1; num++)
                {
                    twt = twt + wtp[num];
                }
                awt = twt / np;
                MessageBox.Show("Average waiting time for " + np + " processes" + " = " + awt + " sec(s)", "Average Awaiting Time", MessageBoxButtons.OK, MessageBoxIcon.None);
            }
            else if (result == DialogResult.No)
            {
                //this.Hide();
                //Form1 frm = new Form1();
                //frm.ShowDialog();
            }
        }

        public static void sjfAlgorithm(string userInput)
        {
            int np = Convert.ToInt16(userInput);

            double[] bp = new double[np];
            double[] wtp = new double[np];
            double[] p = new double[np];
            double twt = 0.0, awt; 
            int x, num;
            double temp = 0.0;
            bool found = false;

            DialogResult result = MessageBox.Show("Shortest Job First Scheduling", "", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (result == DialogResult.Yes)
            {
                for (num = 0; num <= np - 1; num++)
                {
                    string input =
                        Microsoft.VisualBasic.Interaction.InputBox("Enter burst time: ",
                                                           "Burst time for P" + (num + 1),
                                                           "",
                                                           -1, -1);

                    bp[num] = Convert.ToInt64(input);
                }
                for (num = 0; num <= np - 1; num++)
                {
                    p[num] = bp[num];
                }
                for (x = 0; x <= np - 2; x++)
                {
                    for (num = 0; num <= np - 2; num++)
                    {
                        if (p[num] > p[num + 1])
                        {
                            temp = p[num];
                            p[num] = p[num + 1];
                            p[num + 1] = temp;
                        }
                    }
                }
                for (num = 0; num <= np - 1; num++)
                {
                    if (num == 0)
                    {
                        for (x = 0; x <= np - 1; x++)
                        {
                            if (p[num] == bp[x] && found == false)
                            {
                                wtp[num] = 0;
                                MessageBox.Show("Waiting time for P" + (x + 1) + " = " + wtp[num], "Waiting time:", MessageBoxButtons.OK, MessageBoxIcon.None);
                                //Console.WriteLine("\nWaiting time for P" + (x + 1) + " = " + wtp[num]);
                                bp[x] = 0;
                                found = true;
                            }
                        }
                        found = false;
                    }
                    else
                    {
                        for (x = 0; x <= np - 1; x++)
                        {
                            if (p[num] == bp[x] && found == false)
                            {
                                wtp[num] = wtp[num - 1] + p[num - 1];
                                MessageBox.Show("Waiting time for P" + (x + 1) + " = " + wtp[num], "Waiting time", MessageBoxButtons.OK, MessageBoxIcon.None);
                                //Console.WriteLine("\nWaiting time for P" + (x + 1) + " = " + wtp[num]);
                                bp[x] = 0;
                                found = true;
                            }
                        }
                        found = false;
                    }
                }
                for (num = 0; num <= np - 1; num++)
                {
                    twt = twt + wtp[num];
                }
                MessageBox.Show("Average waiting time for " + np + " processes" + " = " + (awt = twt / np) + " sec(s)", "Average waiting time", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public static void priorityAlgorithm(string userInput)
        {
            int np = Convert.ToInt16(userInput);

            DialogResult result = MessageBox.Show("Priority Scheduling ", "", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (result == DialogResult.Yes)
            {
                double[] bp = new double[np];
                double[] wtp = new double[np + 1];
                int[] p = new int[np];
                int[] sp = new int[np];
                int x, num;
                double twt = 0.0;
                double awt;
                int temp = 0;
                bool found = false;
                for (num = 0; num <= np - 1; num++)
                {
                    string input =
                        Microsoft.VisualBasic.Interaction.InputBox("Enter burst time: ",
                                                           "Burst time for P" + (num + 1),
                                                           "",
                                                           -1, -1);

                    bp[num] = Convert.ToInt64(input);
                }
                for (num = 0; num <= np - 1; num++)
                {
                    string input2 =
                        Microsoft.VisualBasic.Interaction.InputBox("Enter priority: ",
                                                           "Priority for P" + (num + 1),
                                                           "",
                                                           -1, -1);

                    p[num] = Convert.ToInt16(input2);
                }
                for (num = 0; num <= np - 1; num++)
                {
                    sp[num] = p[num];
                }
                for (x = 0; x <= np - 2; x++)
                {
                    for (num = 0; num <= np - 2; num++)
                    {
                        if (sp[num] > sp[num + 1])
                        {
                            temp = sp[num];
                            sp[num] = sp[num + 1];
                            sp[num + 1] = temp;
                        }
                    }
                }
                for (num = 0; num <= np - 1; num++)
                {
                    if (num == 0)
                    {
                        for (x = 0; x <= np - 1; x++)
                        {
                            if (sp[num] == p[x] && found == false)
                            {
                                wtp[num] = 0;
                                MessageBox.Show("Waiting time for P" + (x + 1) + " = " + wtp[num], "Waiting time", MessageBoxButtons.OK);
                                //Console.WriteLine("\nWaiting time for P" + (x + 1) + " = " + wtp[num]);
                                temp = x;
                                p[x] = 0;
                                found = true;
                            }
                        }
                        found = false;
                    }
                    else
                    {
                        for (x = 0; x <= np - 1; x++)
                        {
                            if (sp[num] == p[x] && found == false)
                            {
                                wtp[num] = wtp[num - 1] + bp[temp];
                                MessageBox.Show("Waiting time for P" + (x + 1) + " = " + wtp[num], "Waiting time", MessageBoxButtons.OK);
                                //Console.WriteLine("\nWaiting time for P" + (x + 1) + " = " + wtp[num]);
                                temp = x;
                                p[x] = 0;
                                found = true;
                            }
                        }
                        found = false;
                    }
                }
                for (num = 0; num <= np - 1; num++)
                {
                    twt = twt + wtp[num];
                }
                MessageBox.Show("Average waiting time for " + np + " processes" + " = " + (awt = twt / np) + " sec(s)", "Average waiting time", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //Console.WriteLine("\n\nAverage waiting time: " + (awt = twt / np));
                //Console.ReadLine();
            }
            else
            {
                //this.Hide();
            }
        }

        public static void roundRobinAlgorithm(string userInput)
        {
            int np = Convert.ToInt16(userInput);
            int i, counter = 0;
            double total = 0.0;
            double timeQuantum;
            double waitTime = 0, turnaroundTime = 0;
            double averageWaitTime, averageTurnaroundTime;
            double[] arrivalTime = new double[10];
            double[] burstTime = new double[10];
            double[] temp = new double[10];
            int x = np;

            DialogResult result = MessageBox.Show("Round Robin Scheduling", "", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (result == DialogResult.Yes)
            {
                for (i = 0; i < np; i++)
                {
                    string arrivalInput =
                            Microsoft.VisualBasic.Interaction.InputBox("Enter arrival time: ",
                                                               "Arrival time for P" + (i + 1),
                                                               "",
                                                               -1, -1);

                    arrivalTime[i] = Convert.ToInt64(arrivalInput);

                    string burstInput =
                            Microsoft.VisualBasic.Interaction.InputBox("Enter burst time: ",
                                                               "Burst time for P" + (i + 1),
                                                               "",
                                                               -1, -1);

                    burstTime[i] = Convert.ToInt64(burstInput);

                    temp[i] = burstTime[i];
                }
                string timeQuantumInput =
                            Microsoft.VisualBasic.Interaction.InputBox("Enter time quantum: ", "Time Quantum",
                                                               "",
                                                               -1, -1);

                timeQuantum = Convert.ToInt64(timeQuantumInput);
                Helper.QuantumTime = timeQuantumInput;

                for (total = 0, i = 0; x != 0;)
                {
                    if (temp[i] <= timeQuantum && temp[i] > 0)
                    {
                        total = total + temp[i];
                        temp[i] = 0;
                        counter = 1;
                    }
                    else if (temp[i] > 0)
                    {
                        temp[i] = temp[i] - timeQuantum;
                        total = total + timeQuantum;
                    }
                    if (temp[i] == 0 && counter == 1)
                    {
                        x--;
                        //printf("nProcess[%d]tt%dtt %dttt %d", i + 1, burst_time[i], total - arrival_time[i], total - arrival_time[i] - burst_time[i]);
                        MessageBox.Show("Turnaround time for Process " + (i + 1) + " : " + (total - arrivalTime[i]), "Turnaround time for Process " + (i + 1), MessageBoxButtons.OK);
                        MessageBox.Show("Wait time for Process " + (i + 1) + " : " + (total - arrivalTime[i] - burstTime[i]), "Wait time for Process " + (i + 1), MessageBoxButtons.OK);
                        turnaroundTime = (turnaroundTime + total - arrivalTime[i]);
                        waitTime = (waitTime + total - arrivalTime[i] - burstTime[i]);                        
                        counter = 0;
                    }
                    if (i == np - 1)
                    {
                        i = 0;
                    }
                    else if (arrivalTime[i + 1] <= total)
                    {
                        i++;
                    }
                    else
                    {
                        i = 0;
                    }
                }
                averageWaitTime = Convert.ToInt64(waitTime * 1.0 / np);
                averageTurnaroundTime = Convert.ToInt64(turnaroundTime * 1.0 / np);
                MessageBox.Show("Average wait time for " + np + " processes: " + averageWaitTime + " sec(s)", "", MessageBoxButtons.OK);
                MessageBox.Show("Average turnaround time for " + np + " processes: " + averageTurnaroundTime + " sec(s)", "", MessageBoxButtons.OK);
            }
        }

        public static Dictionary<string, double> srtfAlgorithm(string userInput)
        {
            Dictionary<string, double> metrics = new Dictionary<string, double>
            {
                { "AWT", 0 }, { "ATT", 0 }, { "CPU_Utilization", 0 }, { "Throughput", 0 }
            };
            int np;
            try 
            {
                np = Convert.ToInt16(userInput);
                if(np <= 0) 
                {
                    MessageBox.Show("Number of processes must be positive.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return metrics;
                }
            } 
            catch(FormatException) 
            {
                 MessageBox.Show("Invalid number of processes entered.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return metrics; 
            }

            List<ProcessInfo> processes = new List<ProcessInfo>();
            double totalBurstTime = 0;

            // Input Burst Times
            for(int i = 0; i < np; i++)
            {
                string input = Microsoft.VisualBasic.Interaction.InputBox(
                    $"Enter Burst time for P{i + 1}:",
                    "SRTF - Burst Time", "", -1, -1);
                 if(string.IsNullOrWhiteSpace(input)) 
                 { 
                    MessageBox.Show("Operation cancelled.", "SRTF", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return metrics;
                }
                try 
                {
                    double burstTime = Convert.ToDouble(input);
                    if(burstTime < 0) 
                    {
                         MessageBox.Show($"Burst time for P{i + 1} cannot be negative.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                         return metrics; 
                    }
                    processes.Add(new ProcessInfo { Id = i + 1, BurstTime = burstTime, RemainingTime = burstTime });
                    totalBurstTime += burstTime;
                } 
                catch(FormatException) 
                {
                    MessageBox.Show($"Invalid burst time entered for P{i + 1}.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return metrics;
                } 
                catch(OverflowException) 
                {
                     MessageBox.Show($"Burst time for P{i + 1} is too large.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                     return metrics;
                }
            }

            double currentTime = 0;
            int completedProcesses = 0;
            double totalWaitingTime = 0;
            double totalTurnaroundTime = 0;
            double lastCompletionTime = 0; 

            while(completedProcesses < np)
            {
                ProcessInfo shortestProcess = null;
                double minRemainingTime = double.MaxValue;
                 for(int i = 0; i < processes.Count; i++)
                 {
                    if(!processes[i].IsCompleted && processes[i].RemainingTime > 0 && processes[i].RemainingTime < minRemainingTime)
                    {
                        minRemainingTime = processes[i].RemainingTime;
                        shortestProcess = processes[i];
                    }
                 }

                if(shortestProcess == null) 
                {
                    bool allRemainingAreZeroOrCompleted = true;
                    for(int i = 0; i < processes.Count; i++) 
                    {
                        if(!processes[i].IsCompleted && processes[i].RemainingTime > 0) 
                        {
                             allRemainingAreZeroOrCompleted = false;
                             break;
                        }
                     }

                     if(allRemainingAreZeroOrCompleted && completedProcesses < np) 
                     {
                          for(int i = 0; i < processes.Count; i++) 
                          {
                            if (!processes[i].IsCompleted && processes[i].BurstTime == 0) 
                            {
                                 processes[i].FinishTime = currentTime; 
                                 processes[i].TurnaroundTime = processes[i].FinishTime; 
                                 processes[i].WaitingTime = 0; 
                                 processes[i].IsCompleted = true;
                                 totalWaitingTime += 0;
                                 totalTurnaroundTime += processes[i].TurnaroundTime;
                                 completedProcesses++;
                            }
                         }
                         if(completedProcesses == np) 
                         {
                            lastCompletionTime = currentTime; 
                            break; 
                         }
                     }

                    if(shortestProcess == null) 
                    {
                         if(completedProcesses < np) 
                         {
                             bool processShouldRunExists = false;
                             for(int i = 0; i < processes.Count; i++) 
                             {
                                if (!processes[i].IsCompleted && processes[i].RemainingTime > 0) 
                                {
                                    processShouldRunExists = true;
                                    break;
                                }
                            }
                            if (!processShouldRunExists) 
                            {
                                 lastCompletionTime = currentTime; 
                                 break;
                            } 
                            else 
                            {
                                currentTime++; 
                                continue;
                            }
                        } 
                        else 
                        { 
                             lastCompletionTime = currentTime;
                             break;
                        }
                    }
                }

                shortestProcess.RemainingTime--;
                currentTime++;

                if(shortestProcess.RemainingTime == 0)
                {
                    shortestProcess.IsCompleted = true;
                    shortestProcess.FinishTime = currentTime;
                    shortestProcess.TurnaroundTime = shortestProcess.FinishTime; 
                    shortestProcess.WaitingTime = shortestProcess.TurnaroundTime - shortestProcess.BurstTime;
                    if(shortestProcess.WaitingTime < 0) shortestProcess.WaitingTime = 0; 
                    
                    totalWaitingTime += shortestProcess.WaitingTime;
                    totalTurnaroundTime += shortestProcess.TurnaroundTime;
                    completedProcesses++;
                    lastCompletionTime = currentTime; 
                }
            }

            if(np > 0)
            {
                metrics["AWT"] = totalWaitingTime / np;
                metrics["ATT"] = totalTurnaroundTime / np;
                double totalSimulationTime = lastCompletionTime; 

                if(totalSimulationTime > 0) 
                {
                    metrics["CPU_Utilization"] = (totalBurstTime / totalSimulationTime) * 100.0;
                    metrics["Throughput"] = np / totalSimulationTime;
                } 
                else 
                {
                     metrics["CPU_Utilization"] = (totalBurstTime > 0) ? 0 : 100.0; 
                     metrics["Throughput"] = 0; 
                     if(np > 0 && totalBurstTime == 0) metrics["CPU_Utilization"] = 0; 
                }
            }
            return metrics;
        }

        public static Dictionary<string, double> hrrnAlgorithm(string userInput)
        {
             Dictionary<string, double> metrics = new Dictionary<string, double>
            {
                { "AWT", 0 }, { "ATT", 0 }, { "CPU_Utilization", 0 }, { "Throughput", 0 }
            };
            int np;
             try 
             {
                np = Convert.ToInt16(userInput);
                if(np <= 0) 
                {
                    MessageBox.Show("Number of processes must be positive.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return metrics;
                }
            } 
            catch(FormatException) 
            {
                 MessageBox.Show("Invalid number of processes entered.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return metrics;
            }

            List<ProcessInfo> processes = new List<ProcessInfo>();
            double totalBurstTime = 0;

            for(int i = 0; i < np; i++)
            {
                string input = Microsoft.VisualBasic.Interaction.InputBox(
                    $"Enter Burst time for P{i + 1}:",
                    "HRRN - Burst Time", "", -1, -1);
                 if(string.IsNullOrWhiteSpace(input)) 
                 { 
                    MessageBox.Show("Operation cancelled.", "HRRN", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return metrics;
                }
                try 
                {
                    double burstTime = Convert.ToDouble(input);
                    if(burstTime < 0) 
                    {
                         MessageBox.Show($"Burst time for P{i + 1} cannot be negative.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                         return metrics;
                    }
                     processes.Add(new ProcessInfo { Id = i + 1, BurstTime = burstTime, RemainingTime = burstTime });
                     if(burstTime > 0) 
                     {
                         totalBurstTime += burstTime;
                     }
                 } 
                 catch(FormatException) 
                 {
                     MessageBox.Show($"Invalid burst time entered for P{i + 1}.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                     return metrics;
                 } 
                 catch(OverflowException) 
                 {
                      MessageBox.Show($"Burst time for P{i + 1} is too large.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                      return metrics;
                 }
            }

            double currentTime = 0;
            int completedProcesses = 0;
            double totalWaitingTime = 0;
            double totalTurnaroundTime = 0;
            double lastCompletionTime = 0;

            List<ProcessInfo> readyQueue = new List<ProcessInfo>();
            foreach(var proc in processes)
            {
                if(proc.BurstTime == 0) 
                {
                    proc.StartTime = 0;
                    proc.FinishTime = 0;
                    proc.WaitingTime = 0;
                    proc.TurnaroundTime = 0;
                    proc.IsCompleted = true;
                    completedProcesses++;
                    totalWaitingTime += proc.WaitingTime;
                    totalTurnaroundTime += proc.TurnaroundTime;
                } 
                else 
                {
                    readyQueue.Add(proc); 
                }
            }
            if(completedProcesses == np) lastCompletionTime = 0; 

            while(completedProcesses < np)
            {
                 if(readyQueue.Count == 0) 
                 {
                     if(completedProcesses < np) 
                     {
                        lastCompletionTime = currentTime; 
                        break; 
                     }
                     else 
                     {
                        lastCompletionTime = currentTime; 
                        break; 
                     }
                }

                ProcessInfo highestRatioProcess = null;
                double maxRatio = -1.0;

                foreach (var proc in readyQueue)
                {
                    double waitingTime = currentTime; 
                    double burstTime = proc.BurstTime; 
                    double ratio = (waitingTime + burstTime) / burstTime; 

                    if(ratio > maxRatio)
                    {
                        maxRatio = ratio;
                        highestRatioProcess = proc;
                    }
                }

                if(highestRatioProcess == null) 
                {
                    break;
                }

                highestRatioProcess.StartTime = currentTime; 
                currentTime += highestRatioProcess.BurstTime; 
                highestRatioProcess.FinishTime = currentTime;
                lastCompletionTime = currentTime; 

                highestRatioProcess.TurnaroundTime = highestRatioProcess.FinishTime; 
                highestRatioProcess.WaitingTime = highestRatioProcess.TurnaroundTime - highestRatioProcess.BurstTime;
                 if(highestRatioProcess.WaitingTime < 0) highestRatioProcess.WaitingTime = 0; 

                totalWaitingTime += highestRatioProcess.WaitingTime;
                totalTurnaroundTime += highestRatioProcess.TurnaroundTime;
                highestRatioProcess.IsCompleted = true;
                completedProcesses++;
                readyQueue.Remove(highestRatioProcess); 
            }

            if(np > 0)
            {
                metrics["AWT"] = totalWaitingTime / np;
                metrics["ATT"] = totalTurnaroundTime / np;
                double totalSimulationTime = lastCompletionTime;

                if(totalSimulationTime > 0) 
                {
                    metrics["CPU_Utilization"] = (totalBurstTime / totalSimulationTime) * 100.0;
                    metrics["Throughput"] = np / totalSimulationTime;
                } 
                else 
                {
                    metrics["CPU_Utilization"] = (totalBurstTime > 0) ? 0 : 100.0; 
                    metrics["Throughput"] = 0; 
                    if(np > 0 && totalBurstTime == 0) metrics["CPU_Utilization"] = 0;
                }
            }
            return metrics;
        }
    }
}

