using PMM01500COMMON;
using R_BlazorFrontEnd.Exceptions;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace PMM01500MODEL
{
    public class PMM01522ViewModel
    {
        private PMM01520Model _PMM01520Model = new PMM01520Model();

        public ObservableCollection<PMM01522DTO> LookupAdditional { get; set; } = new ObservableCollection<PMM01522DTO>();


    }

}
