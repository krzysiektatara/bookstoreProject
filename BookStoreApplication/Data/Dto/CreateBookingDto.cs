﻿namespace BookStoreApplicationAPI.Data.Dto
{
    public class CreateBookingDto
    {
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public string? Delivery_Address { get; set; }
        public int Quantity { get; set; }
    }
}
