using ClinicManagementSystem.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManagementSystem.Logic
{
    public class clsPrescriptionItem
    {
        public int ID { get; private set; }
        public int PrescriptionID { get; set; }
        public int MedicationID { get; set; }
        public double Dosage { get; set; }
        public string DosageType { get; set; }
        public string DosageNote { get; set; }

        public clsPrescriptionItem()
        {
            this.ID = -1;
            MedicationID = -1;
            Dosage = -1;
            PrescriptionID = -1;
            DosageType = "";
            DosageNote = "";
        }

        public bool AddNewPrescriptionItem()
        {
            int ID = clsPrescriptionItemData.AddNewPrescriptionItem(this.PrescriptionID, this.MedicationID, this.Dosage, this.DosageType
                , this.DosageNote);

            if (ID != -1)
            {
                this.ID = ID;
                return true;
            }

            return false;
        }

        public static bool DeletePrescriptionItem(int id)
        {
            return clsPrescriptionItemData.DeletePrescriptionItem(id);
        }

        public static DataTable GetAllPrescriptionItemsForPrescriptionByID(int PrescriptionId)
        {
            return clsPrescriptionItemData.GetAllByPrescriptionID(PrescriptionId);
        }
    }
}
