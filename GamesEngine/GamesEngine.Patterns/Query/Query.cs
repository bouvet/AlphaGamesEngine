﻿using GamesEngine.Patterns.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamesEngine.Patterns.Query
{
    public interface IQuery : IMessage
    {
        Guid Id { get; }
        string Type { get; }
    }
}