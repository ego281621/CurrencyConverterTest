using System;
using System.Collections.Generic;

namespace CurrencyConverterTest.Core.Entities;

public partial class RequestLog
{
    public int Id { get; set; }

    public string? Url { get; set; }

    public string? Method { get; set; }

    public string? RequestBody { get; set; }

    public int? StatusCode { get; set; }

    public DateTime? Timestamp { get; set; }
}
