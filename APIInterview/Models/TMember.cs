﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace APIInterview.Models;

public partial class TMember
{
    public int FMemberId { get; set; }

    public string FName { get; set; }

    public string FPhone { get; set; }

    public string FEmail { get; set; }

    public string FAddress { get; set; }

    public string FPassword { get; set; }

    public virtual ICollection<TArticle> TArticles { get; set; } = new List<TArticle>();

    public virtual ICollection<TMessage> TMessages { get; set; } = new List<TMessage>();
}