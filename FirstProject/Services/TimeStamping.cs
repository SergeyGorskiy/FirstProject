using System;

namespace FirstProject.Services
{
    public interface ITimeStamper
    {
        public string TimeStamp { get; }
    }

    public class DefaultTimeStamper : ITimeStamper
    {
        public string TimeStamp { get => DateTime.Now.ToShortTimeString(); }
    }
}