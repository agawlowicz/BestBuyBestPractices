using System;
using System.Collections.Generic;

namespace BestBuyBestPractices
{
    public interface IDepartmentRepository
    {
        public IEnumerable<Department> GetAllDepartments(); //stubbed out method
    }
}
