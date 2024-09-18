﻿using Core.Utilities.Results;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IMealService
    {
        IDataResult<List<Meal>> GetAll();
        IResult Add(IFormFile file, Meal meal);
        IResult Update(IFormFile file, Meal meal);
        IResult Delete(Meal meal);
    }
}
