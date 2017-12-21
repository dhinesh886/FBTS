using System;

namespace FBTS.Model.Common.CustomEventArgs
{
    public class CommonEventArgs : EventArgs
    {
        public string Key { get; set; }
        public string SubKey { get; set; }
        public string SecondaryKey { get; set; }
    }
}
