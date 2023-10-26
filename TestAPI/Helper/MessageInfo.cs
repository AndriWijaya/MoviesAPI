using System.Collections.Generic;

namespace TestAPI.Helper
{
    public class MessageInfo
    {
        public bool Status { get; set; } = false;
        public List<string> Message { get; set; } = new List<string>();
    }
}