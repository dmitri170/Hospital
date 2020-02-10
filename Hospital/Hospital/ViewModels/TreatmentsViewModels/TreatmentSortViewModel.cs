using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hospital.Models;

namespace Hospital.ViewModels.TreatmentsViewModels
{
    public class TreatmentSortViewModel
    {
        public SortState TreatmentIdSort { get; private set; }
        public SortState NameDepartmentSort { get; private set; }
        public SortState PatientSurnamesSort { get; private set; }
        public SortState DoctorSurnamesSort { get; private set; }
        public SortState ReceiptDateSort { get; private set; }
        public SortState DischargeDateSort { get; private set; }
        public SortState DiagnosisSort { get; private set; }
        public SortState Current { get; private set; }

        public TreatmentSortViewModel(SortState sortOrder)
        {
            TreatmentIdSort = sortOrder == SortState.TreatmentIdAsc ? SortState.TreatmentIdDesc : SortState.TreatmentIdAsc;
            NameDepartmentSort = sortOrder == SortState.NameDepartmentAsc ? SortState.NameDepartmentDesc : SortState.NameDepartmentAsc;
            PatientSurnamesSort = sortOrder == SortState.PatientSurnamesAsc ? SortState.PatientSurnamesDesc : SortState.PatientSurnamesAsc;
            DoctorSurnamesSort = sortOrder == SortState.DoctorSurnameAsc ? SortState.DoctorSurnameDesc : SortState.DoctorSurnameAsc;
            ReceiptDateSort = sortOrder == SortState.ReceiptDateAsc ? SortState.ReceiptDateDesc : SortState.ReceiptDateAsc;
            DischargeDateSort = sortOrder == SortState.DischargeDateAsc ? SortState.DischargeDateDesc : SortState.DischargeDateAsc;
            DiagnosisSort = sortOrder == SortState.DiagnosisAsc ? SortState.DiagnosisDesc : SortState.DiagnosisAsc;
            Current = sortOrder;
        }

    }
}
