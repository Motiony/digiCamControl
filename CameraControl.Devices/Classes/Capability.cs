namespace CameraControl.Devices.Classes
{
    public class Capability<T>
    {
        /// <summary>
        /// Current value of the specified parameter.
        /// </summary>
        public virtual T Current { set; get; }

        /// <summary>
        /// Candidate values of the specified parameter.
        /// </summary>
        public virtual List<T> Candidates { set; get; }
    }
}
