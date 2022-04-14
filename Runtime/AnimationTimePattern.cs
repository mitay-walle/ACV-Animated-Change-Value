namespace Plugins.mitaywalle.ACV.Runtime
{
    public enum AnimationTimePattern
    {
        /// <summary>
        /// <see cref="ACV_Base._fixedTime"/> - fixed amount of time to change from 1 to 2 and, same as from 1 to 1000
        /// </summary>
        FixedTime = 0,
        /// <summary>
        /// <see cref="ACV_Base._speed"/> - fixed amount of <see cref="ACV_Base._value"/> changed per frame  
        /// </summary>
        Speed = 1
    }
}
