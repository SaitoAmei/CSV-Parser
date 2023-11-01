using System;
using System.Collections.Generic;

namespace CSV_Parser;

public partial class OrdersHistory
{
    public int Id { get; set; }

    public DateTime TpepPickupDatetime { get; set; }

    public DateTime TpepDropoffDatetime { get; set; }

    public int PassengerCount { get; set; }

    public double TripDistance { get; set; }

    public string? StoreAndFwdFlag { get; set; }

    public int PulocationId { get; set; }

    public int DolocationId { get; set; }

    public decimal FareAmount { get; set; }

    public decimal TipAmount { get; set; }
}
