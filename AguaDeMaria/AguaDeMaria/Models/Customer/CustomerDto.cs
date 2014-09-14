﻿namespace AguaDeMaria.Models.Customer
{
    public class CustomerDto
    {
        public int CustomerId { get; set; }
        public string CustomerCode { get; set; }
        public string CustomerName { get; set; }
        public int CustomerTypeId { get; set; }
        public string CustomerTypeName { get; set; }
        public string Address { get; set; }
        public string ContactNumbers { get; set; }
        public string Notes { get; set; }

    }
}