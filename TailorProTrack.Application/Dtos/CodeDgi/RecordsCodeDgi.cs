using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TailorProTrack.Application.Dtos.CodeDgi
{


        public record CodeDgiDtoAdd
        {
            public int INITIAL_NUMBER { get; set; }
            public int END_NUMBER { get; set; }
        }

        public record CodeDgiDtoUpdate
        {
            public int ID { get; set; }
            public int INITIAL_NUMBER { get; set; }
            public int END_NUMBER { get; set; }
        }

        public record CodeDgiDtoGet
        {
            public int ID { get; set; }
            public int INITIAL_NUMBER { get; set;}
            public int END_NUMBER { get; set;}
            public int CURRENT_NUMBER { get; set; }
        }
}
