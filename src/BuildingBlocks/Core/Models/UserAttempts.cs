using System;
using System.ComponentModel.DataAnnotations;

namespace BuildingBlocks.EFCore
{
    public class UserAttempts
    {
        public int Id { get; set; }
        public string Username { get; set; } = default;
        public DateTime RequestMadeOn { get; set; }
    }
}