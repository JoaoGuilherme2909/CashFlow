﻿using CashFlow.Communication.Enums;

namespace CashFlow.Communication.Requests;

public class RequestRegisterExpenseJson
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; }
    public DateTime Date { get; set; }
    public decimal Amount { get; set; }
    public PaymentType paymentType { get; set; }
}
