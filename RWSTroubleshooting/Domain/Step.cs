using System.Collections.Generic;

namespace RWSTroubleshooting.Domain
{
    public class Step
    {
        public string Text { get; set; }

        public IList<Option> Options { get; set; }
    }
}
