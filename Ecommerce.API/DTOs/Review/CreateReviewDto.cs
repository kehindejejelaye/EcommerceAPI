﻿using Ecommerce.API.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.API.DTOs.Review;

public class CreateReviewDto
{
    public string Comment { get; set; }

    [Range(1, 5)]
    public int RatingValue { get; set; }
    public string ProductItemId { get; set; }
    public string UserId { get; set; }
}
