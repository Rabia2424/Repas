using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Business.Concrete
{
    public class MealManager : IMealService
    {
        private IMealDal _mealDal;
        public MealManager(IMealDal mealDal)
        {
            _mealDal = mealDal;
        }
        public IResult Add(IFormFile file, Meal meal)
        {
            if (!IsMealNameAlreadyExist(meal.Name))
            {
                if (file == null || file.Length == 0)
                {
                    return new ErrorResult("File is empty!");
                }

                var filePath = Path.Combine("wwwroot", "Uploads", "Images", Guid.NewGuid().ToString() + Path.GetExtension(file.FileName));

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                meal.ImageUrl = filePath.Replace("wwwroot", ""); ;

                if (meal.Name.Length < 3)
                {
                    return new ErrorResult(Messages.MealNotAdded);
                }
                _mealDal.Add(meal);
                return new SuccessResult(Messages.MealAdded);
            }
            else
            {
                return new ErrorResult(Messages.MealAlreadyExist);
            }
           
        }

        public IResult Delete(Meal meal)
        {
            var mealToDelete = _mealDal.Get(m => m.Id == meal.Id);
            if (mealToDelete == null)
            {
                return new ErrorResult(Messages.MealNotFound);
            }
            var fullPath = Path.Combine("wwwroot", mealToDelete.ImageUrl.Replace("\\", "/").TrimStart('/'));
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
                _mealDal.Delete(mealToDelete);
                return new SuccessResult(Messages.MealDeleted);
            }
            return new ErrorResult(Messages.MealImageUrlNotFound);
        }

        public IDataResult<List<Meal>> GetAll()
        {
            return new SuccessDataResult<List<Meal>>(_mealDal.GetAll());
        }

        public IResult Update(IFormFile file, Meal meal)
        {
            var mealToUpdate = _mealDal.Get(m => m.Id == meal.Id);

            if (file != null && file.Length > 0)
            {
                var newFilePath = Path.Combine("wwwroot", "Uploads", "Images", Guid.NewGuid().ToString() + Path.GetExtension(file.FileName));

                if (File.Exists(mealToUpdate.ImageUrl))
                {
                    File.Delete(mealToUpdate.ImageUrl);
                }

                // Save new image
                using (var stream = new FileStream(newFilePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                mealToUpdate.ImageUrl = newFilePath.Replace("wwwroot", "");
                mealToUpdate.Name = meal.Name;
                mealToUpdate.Description = meal.Description;
                mealToUpdate.Price = meal.Price;
                mealToUpdate.Category = meal.Category;
                _mealDal.Update(mealToUpdate);  

                return new SuccessResult(Messages.MealUpdated);
            }
            return new ErrorResult("Invalid File!");
        }

        private bool IsMealNameAlreadyExist(string name)
        {
            var meal = _mealDal.Get(m => m.Name == name);
            if(meal != null)
            {
                return true;
            }
            return false;   
        }
    }
}
