using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AuditionProj.Models
{
    public class MessageModel
    {
        public long ID { get; set; }
        public string MessageContent { get; set; }
        public bool IsPalindrome { get; set; }
    }
}