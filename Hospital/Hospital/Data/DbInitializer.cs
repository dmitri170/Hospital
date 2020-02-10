using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hospital.Models;

namespace Hospital.Data
{
    public class DbInitializer
    {
        public static void Initialize(HospitalContext db)
        {
            db.Database.EnsureCreated();

            if (db.Departments.Any())
            {
                return;
            }


            int departments_number = 35;
            int treatment_number = 300;
            int doctors_number = 300;
            int servises_number = 300;
            int pacients_number = 35;
            int provisionofpaidservices_number = 300;
            string patientSurname;
            string patientName;
            string doctorSurname;
            string doctorName;
            string nameDepartment;
            int numberPlace;
            string specialties;
            string categories;
            int numberPalat;
            string adres;
            DateTime dateOfServiceProvision;
            string nameType;
            int priceService;
            DateTime datePrice;
            string diagnosis;
            DateTime receiptDate;
            DateTime dischargeDate;
            string voc = "QWERTYUIOPASDFGHJKLZXCVBNMqwertyuiopasdfghjklzxcvbnm";


            Random randObj = new Random(1);
            string[] department_voc = { "Хирургия", "Травматология", "Терапевтия", "Стоматология","Венерологическое" };// словарь названий отделений
            string[] diagnosis_voc = { "Орви", "Простуда", "Грипп" };//Словарь названий заболеваний
            int count_department_voc = department_voc.GetLength(0);
            int count_diagnosis_voc = diagnosis_voc.GetLength(0);
            for (int departmentID = 1; departmentID <= departments_number; departmentID++)
            {
                nameDepartment = department_voc[randObj.Next(count_department_voc)] + department_voc.ToString();
                numberPlace = randObj.Next(1, 10);
                db.Departments.Add(new Department { NameDepartments = nameDepartment, NumberPlace = numberPlace });
            }
            db.SaveChanges();
            string[] specialties_voc = { "Хирург", "Травматолог", "Терапевт", "Венеролог","Стоматолог" };
            int count_specialties_voc = specialties_voc.GetLength(0);
            for (int doctorsID = 1; doctorsID < doctors_number; doctorsID++)
            {
                int departmentID = randObj.Next(1, doctors_number - 1);
                doctorSurname = GenRandomString(voc, 15);
                doctorName = GenRandomString(voc, 15);
                specialties = specialties_voc[randObj.Next(count_specialties_voc)]+doctorsID.ToString();
                categories = GenRandomString(voc, 15);
                db.Doctors.Add(new Doctor { DepartmentId = departmentID, DoctorNames = doctorName, DoctorSurnames = doctorSurname, Specialties = specialties, Categories = categories });
            }
            db.SaveChanges();
            for(int pacientsID=1;pacientsID<pacients_number;pacientsID++)
            {
                patientName = GenRandomString(voc, 15);
                patientSurname = GenRandomString(voc, 15);
                numberPalat = randObj.Next(1, 10);
                adres = GenRandomString(voc, 15);
                db.Pacients.Add(new Pacient { PatientNames = patientName, PatientSurnames = patientSurname, NumberPalat = numberPalat, Adres = adres });
            }
            db.SaveChanges();
            for(int provisionID=1;provisionID<=provisionofpaidservices_number;provisionID++)
            {
                int doctorsID = randObj.Next(1, doctors_number - 1);
                int servisesID = randObj.Next(1, servises_number - 1);
                int pacientsID = randObj.Next(1, pacients_number - 1);
                dateOfServiceProvision = DateTime.Now.Date;
                db.ProvisionOfPaidServices.Add(new ProvisionOfPaidService { DoctorsId = doctorsID, PacientsId = pacientsID, DateOfServiceProvision = dateOfServiceProvision });
            }
            db.SaveChanges();
            for(int servisesID=1;servisesID<=servises_number;servisesID++)
            {
                nameType = GenRandomString(voc,15);
                priceService = randObj.Next(1, 100);
                int provisionID = randObj.Next(1, provisionofpaidservices_number - 1);
                datePrice = DateTime.Now.Date;
                db.Servises.Add(new Servise { NameType=nameType,PriceService=priceService,ProvisionId=provisionID,DataPrice=datePrice});

            }
            db.SaveChanges();
            for(int treatmentID=1;treatmentID<=treatment_number;treatmentID++)
            {
                diagnosis = diagnosis_voc[randObj.Next(count_diagnosis_voc)] + diagnosis_voc.ToString();
                int doctorsID = randObj.Next(1, doctors_number - 1);
                int pacientsID = randObj.Next(1, pacients_number - 1);
                int departmentID = randObj.Next(1, departments_number - 1);
                DateTime today = DateTime.Now.Date;
                receiptDate = today.AddDays(-treatmentID);
                dischargeDate = DateTime.Now;
                db.Treatment.Add(new Treatment { Diagnosis = diagnosis, DoctorsId = doctorsID, PacientsId = pacientsID, DepartmentId = departmentID, ReceiptDate = receiptDate,DischargeDate=dischargeDate });
            }
            db.SaveChanges();
        }
            static string GenRandomString(string Alphabet, int Length)
            {
                Random rnd = new Random();
                //объект StringBuilder с заранее заданным размером буфера под результирующую строку
                StringBuilder sb = new StringBuilder(Length - 1);
                //переменную для хранения случайной позиции символа из строки Alphabet
                int Position = 0;
                string ret = "";
                for (int i = 0; i < Length; i++)
                {
                    //получаем случайное число от 0 до последнего
                    //символа в строке Alphabet
                    Position = rnd.Next(0, Alphabet.Length - 1);
                    //добавляем выбранный символ в объект
                    //StringBuilder
                    ret = ret + Alphabet[Position];
                }
                return ret;
            }
        

    }
}