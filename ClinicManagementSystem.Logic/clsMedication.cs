using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClinicManagementSystem.Data;

namespace ClinicManagementSystem.Logic
{
    public class clsMedication
    {
        public int MedicationID { get; set; }
        public string MedicationName { get; set; }
        public string MedicationSerialNumber { get; set; }
        public string Description { get; set; }

        public clsMedication()
        {
            MedicationID = -1;
            MedicationName = string.Empty;
            MedicationSerialNumber = string.Empty;
            Description = string.Empty;
        }

        public bool AddNewMedication(string MedicationName, string MedicationSerialNumber, string Description)
        {
            int newId = clsMedicationData.AddNewMedication(MedicationName, MedicationSerialNumber, Description);
            if (newId > 0)
            {
                this.MedicationID = newId;
                this.MedicationName = MedicationName;
                this.MedicationSerialNumber = MedicationSerialNumber;
                this.Description = Description;
                return true;
            }
            return false;
        }
    
        public bool DeleteMedication(int MedicationID)
        {
            return clsMedicationData.DeleteMedication(MedicationID);
        }

        public static DataTable GetAllMedications()
        {
            return clsMedicationData.GetAllMedications();
        }

        public static clsMedication GetMedicationByID(int MedicationID)
        {
            clsMedication med = new clsMedication();
            string medName = string.Empty;
            string medSerial = string.Empty;
            string medDesc = string.Empty;

            if (clsMedicationData.GetMedicationByID(MedicationID, ref medName, ref medSerial, ref medDesc))
            {
                med.MedicationID = MedicationID;
                med.MedicationName = medName;
                med.MedicationSerialNumber = medSerial;
                med.Description = medDesc;

                return med;
            }
            return null;
        }

    }
}
