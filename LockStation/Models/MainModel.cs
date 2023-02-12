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
        public DateTime CurTime { get; set; }

        public event EventHandler NeedClose;
        public event EventHandler ShutDown;

        public string TimeString
        {
            get
            { 
                var time = MaxTime - CurTime.AddMinutes(-1);
                if (time.TotalSeconds < 0)
                {
                    NeedClose?.Invoke(this, new EventArgs());
                    return ZERO;
                }
                return new DateTime(time.Ticks).ToString("HH:mm"); 
            }
        }

        public string GrantedTimeString
        {
            get
            {
                return MinTime.ToString("dd.MM.yy HH:mm")+" - "+MaxTime.ToString("HH:mm");
            }
        }

        public MainModel() 
        {
            CurTime = DateTime.Now;
            MinTime = DateTime.Now.Date.AddHours(19);
            MaxTime = DateTime.Now.Date.AddHours(23).AddMinutes(55);
        }

        public bool NeedWarning()
        {
            return false;
        }
    }
}
