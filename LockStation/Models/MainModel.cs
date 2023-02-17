using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LockStation.Models
{
    public enum State { Work, Shutdown };
    public class MainModel
    {
        const string ZERO = "00:00";
        

        public State WorkState { get; set; } = State.Work;
        public bool CanClose { get; set; } = false;

        public int Total = 0;
        public int Elapsed = 0;

        public event EventHandler NeedClose;
        public event EventHandler ShutDown;

        public string TimeString
        {
            get
            {
                int time = Total - Elapsed;
                
                if (time < 0)
                {
                    NeedClose?.Invoke(this, new EventArgs());
                    return ZERO;
                }
                return ConvertMinInString(time);
            }
        }

        public string TimeGranted
        {
            get { return ConvertMinInString(Total); }
        }

        private string ConvertMinInString(int time)
        {
            return $"{time / 60:D2}:{time % 60:D2}";
        }

        public void Tick()
        {
            Elapsed++;
            int time = Total- Elapsed;
            if (time <= 5 && WorkState == State.Work)
            {
                WorkState = State.Shutdown;
                NeedClose?.Invoke(this, new EventArgs());
            }

            if (time < 0) ShutDown?.Invoke(this, new EventArgs());
        }
        public MainModel() 
        {
            Total = 180;
            Elapsed = 0;
        }
    }
}
