using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LockStation.Models
{
    public class MainModel
    {
        const string ZERO = "00:00";
        public bool CanClose { get; set; } = false;
        public DateTime MaxTime { get; set; }
        public DateTime MinTime { get; set; }

        public event EventHandler NeedClose;

        public string TimeString
        {
            get
            { 
                var time = MaxTime- DateTime.Now.AddHours(4);
                if (time.TotalSeconds < 0)
                {
                    NeedClose?.Invoke(this, new EventArgs());
                    return ZERO;
                }
                return new DateTime(time.Ticks).ToString("HH:mm"); 
            }
        }

        public MainModel() 
        {
            MaxTime = DateTime.Now.Date.AddHours(21);
        }
    }
}
