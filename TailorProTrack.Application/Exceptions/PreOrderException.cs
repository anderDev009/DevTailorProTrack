﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TailorProTrack.Application.Exceptions
{
	public class PreOrderException(string message) : Exception(message);
}
