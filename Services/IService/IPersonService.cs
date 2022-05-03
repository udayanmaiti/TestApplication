using System.Collections.Generic;
using TestApplication.Models;

namespace TestApplication.Services.IService
{
    public interface IPersonService
    {
       void AddPerson(PersonViewModel person);
        List<PersonViewModel> GetPersonList();
    }
}
