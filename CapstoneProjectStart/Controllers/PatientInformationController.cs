using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CapstoneProjectStart.Models;
using System.Net;
using System.Data.Entity.Infrastructure;


namespace CapstoneProjectStart.Controllers
{
    public class PatientInformationController : Controller
    {
        // GET: PatientInformation
        
       
        private PatientsDBEntities6 _db = new PatientsDBEntities6();
      
        public ActionResult Index()

        {  
            
            return View(_db.Appointments.ToList());
        }

        public ActionResult ViewDoctorSchedule(Appointment appointment, object selectedDoctor)
        {
            var appointments = from a in _db.Appointments
                               select a;

            var doctorQuery = from d in _db.Practitioners
                              orderby d.Last_Name
                              select d;
            ViewBag.DoctorName = new SelectList(doctorQuery, "DoctorID", "Name", selectedDoctor);

            string doctor = ViewBag.DoctorName;


            appointments = appointments.Where(a => a.Practitioner_==doctor);

            return View(appointments.ToList());


        }

      

        // GET: PatientInformation/Delete/5


        private void PopulateDropDownList(object selectedDoctor = null)
        {
            var doctorQuery = from d in _db.Practitioners
                              orderby d.Last_Name
                              select d;
            ViewBag.DoctorName = new SelectList(doctorQuery, "DoctorID", "Name", selectedDoctor);
        }

        public ViewResult UpcomingAppointments()
        {
            var appointments = from a in _db.Appointments
                               select a;

            var dateandtime = DateTime.Now;
            var todaysDate = dateandtime.Date;
            appointments = appointments.Where(a => a.Date > todaysDate);

            return View(appointments.ToList());

        }

        public ViewResult MissedAppointments()
        {
            var appointments = from a in _db.Appointments
                               select a;

            appointments = appointments.Where(a => a.isMissed == true);

            return View(appointments.ToList());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="practitioner"></param>
        /// <returns></returns>
       // public ViewResult DoctorSchedule([Bind(Include ="LastName")] Practitioner practitioner)
        //{
          //  ViewBag.Last_Name = new SelectList(practitioner.Last_Name, "Last Name");
            
           // var appointments = from a in _db.Appointments select a;

            //appointments = appointments.Where(a => a.Practitioner_ = ViewBag.Last_Name);
        //}

       

        public ActionResult ViewPatientInformation ()
        {

            return View(_db.PatientInformations.ToList());
        }

        public ActionResult ViewPatientMedicalInformation()
        {

            return View(_db.PatientMedicalInformations.ToList());
        }

        public ActionResult ViewPrescriptions()
        {
            return View(_db.Prescriptions.ToList());
        }

        // GET: PatientInformation/DetailsForPatientInformation/id

        public ActionResult DetailsForAppointments(int id)
        {
            
            Appointment appointmentDetail = _db.Appointments.Find(id);

            if (appointmentDetail == null)
            {
                return HttpNotFound();
            }
            return View(appointmentDetail);

        }

        public ActionResult DetailsForPrescriptions(int id)
        {

            Prescription prescriptionDetail = _db.Prescriptions.Find(id);

            if (prescriptionDetail == null)
            {
                return HttpNotFound();
            }
            return View(prescriptionDetail);

        }




        public ActionResult DetailsForPatientInformation(int id)
        {

            PatientInformation patientDetail = _db.PatientInformations.Find(id);

            if(patientDetail == null)
            {
                return HttpNotFound();
            }
            return View(patientDetail);

        }

        public ActionResult DetailsForPatientMedicalInformation(int id)
        {

            PatientMedicalInformation patientMedicalDetail = _db.PatientMedicalInformations.Find(id);

            if (patientMedicalDetail == null)
            {
                return HttpNotFound();
            }
            return View(patientMedicalDetail);

        }

        // GET: PatientInformation/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PatientInformation/Create
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create([Bind(Exclude ="Id")]Appointment appointmentToCreate)
        {
            if (!ModelState.IsValid)
                return View();

            
            _db.Appointments.Add(appointmentToCreate);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult CreateNewMedPatient()
        {
            return View();
        }

        // POST: PatientInformation/Create
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CreateNewMedPatient([Bind(Exclude = "Id")]PatientMedicalInformation patientMedToCreate)
        {
            if (!ModelState.IsValid)
                return View();


            _db.PatientMedicalInformations.Add(patientMedToCreate);
            _db.SaveChanges();
            return RedirectToAction("ViewPatientMedicalInformation");
        }
        public ActionResult CreateNewPrescription()
        {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CreateNewPrescription([Bind(Exclude = "Id")]Prescription prescriptionToCreate)
        {
            if (!ModelState.IsValid)
                return View();


            _db.Prescriptions.Add(prescriptionToCreate);
            _db.SaveChanges();
            return RedirectToAction("ViewPrescriptions");
        }
        // GET: PatientInformation/Delete
        public ActionResult Delete(int? id)
        {
            if(id==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appointment appointmentToDelete = _db.Appointments.Find(id);
            if(appointmentToDelete==null)
            {
                return HttpNotFound();
            }

            return View(appointmentToDelete);
        }

        // POST: PatientInformation/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Appointment appointmentToDelete = _db.Appointments.Find(id);
            _db.Appointments.Remove(appointmentToDelete);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult DeletePatientInformation(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PatientInformation patientinfo = _db.PatientInformations.Find(id);
            if (patientinfo == null)
            {
                return HttpNotFound();
            }

            return View(patientinfo);
        }

        // POST: PatientInformation/Delete
        [HttpPost, ActionName("DeletePatientInformation")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePatientConfirmed(int id)
        {
            PatientInformation patientinfo = _db.PatientInformations.Find(id);
            _db.PatientInformations.Remove(patientinfo);
            _db.SaveChanges();
            return RedirectToAction("ViewPatientInformation");
        }

        public ActionResult DeletePatientMedicalInformation(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PatientMedicalInformation patientmedinfo = _db.PatientMedicalInformations.Find(id);
            if (patientmedinfo == null)
            {
                return HttpNotFound();
            }

            return View(patientmedinfo);
        }

        // POST: PatientInformation/Delete
        [HttpPost, ActionName("DeletePatientMedicalInformation")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePatientMedConfirmed(int id)
        {
            PatientMedicalInformation patientinfo = _db.PatientMedicalInformations.Find(id);
            _db.PatientMedicalInformations.Remove(patientinfo);
            _db.SaveChanges();
            return RedirectToAction("ViewPatientMedicalInformation");
        }

        public ActionResult DeletePrescription(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prescription prescriptioninfo = _db.Prescriptions.Find(id);
            if (prescriptioninfo == null)
            {
                return HttpNotFound();
            }

            return View(prescriptioninfo);
        }

        // POST: PatientInformation/Delete
        [HttpPost, ActionName("DeletePrescription")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePrescriptionConfirmed(int id)
        {
            Prescription prescriptioninfo = _db.Prescriptions.Find(id);
            _db.Prescriptions.Remove(prescriptioninfo);
            _db.SaveChanges();
            return RedirectToAction("ViewPrescriptions");
        }

        // GET: PatientInformation/CreateNewPatient
        public ActionResult CreateNewPatient()
        {
            return View();
        }

        // POST: PatientInformation/CreateNewPatient
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CreateNewPatient([Bind(Exclude = "Id")]PatientInformation patientToCreate)
        {
            if (!ModelState.IsValid)
                return View();


            _db.PatientInformations.Add(patientToCreate);
            _db.SaveChanges();
            return RedirectToAction("ViewPatientInformation");
        }

        public ActionResult Edit(int? id)
        {
           

            if (id == null)
            {


                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Appointment appointmentToEdit = _db.Appointments.Find(id);
            if (appointmentToEdit == null)
            {
                return HttpNotFound();
            }

            return View(appointmentToEdit);


        }




        // POST: PatientInformation/Edit/5
        [HttpPost, ActionName("Edit")]
        public ActionResult EditAppointment(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var appointmentToUpdate = _db.Appointments.Find(id);
            if (TryUpdateModel(appointmentToUpdate, "", new string[] { "Patient ID", "Date", "Practitioner Time Start", "Practitioner Time End", "Check In", "Check Out", "Practitioner", "Prescription", "Reason", "Time Slot", "Notes" }))
            {
                try
                {
                    _db.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (RetryLimitExceededException /* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }

            }
            return View(appointmentToUpdate);
        }

        public ActionResult EditPatient(int? id)
        {


            if (id == null)
            {


                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            PatientInformation patientToEdit = _db.PatientInformations.Find(id);
            if (patientToEdit == null)
            {
                return HttpNotFound();
            }

            return View(patientToEdit);


        }




        // POST: PatientInformation/Edit/5
        [HttpPost, ActionName("EditPatient")]
        public ActionResult EditPatientConfirmed(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var patientToUpdate = _db.PatientInformations.Find(id);
            if (TryUpdateModel(patientToUpdate, "", new string[] { "First Name", "Middle Initial", "Last Name", "Date of Birth", "Gender", "Patient ID", "Insurance" }))
            {
                try
                {
                    _db.SaveChanges();

                    return RedirectToAction("ViewPatientInformation");
                }
                catch (RetryLimitExceededException /* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }

            }
            return View(patientToUpdate);
        }

         public ActionResult EditMedPatient(int? id)
        {


            if (id == null)
            {


                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            PatientMedicalInformation patientmedToEdit = _db.PatientMedicalInformations.Find(id);
            if (patientmedToEdit == null)
            {
                return HttpNotFound();
            }

            return View(patientmedToEdit);


        }




        // POST: PatientInformation/Edit/5
        [HttpPost, ActionName("EditMedPatient")]
        public ActionResult EditPatientMedConfirmed(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var patientmedToUpdate = _db.PatientMedicalInformations.Find(id);
            if (TryUpdateModel(patientmedToUpdate, "", new string[] { "First Name", "Last Name", "Referrals", "Prescriptions", "Allergies" }))
            {
                try
                {
                    _db.SaveChanges();

                    return RedirectToAction("ViewPatientMedicalInformation");
                }
                catch (RetryLimitExceededException /* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }

            }
            return View(patientmedToUpdate);
        }

        public ActionResult EditPrescription(int? id)
        {


            if (id == null)
            {


                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Prescription prescriptionToEdit = _db.Prescriptions.Find(id);
            if (prescriptionToEdit == null)
            {
                return HttpNotFound();
            }

            return View(prescriptionToEdit);


        }




        // POST: PatientInformation/Edit/5
        [HttpPost, ActionName("EditPrescription")]
        public ActionResult EditPrescriptionConfirmed(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var prescriptionToUpdate = _db.Prescriptions.Find(id);
            if (TryUpdateModel(prescriptionToUpdate, "", new string[] { "Patient ID", "Prescriptions", "Added On", "Added By", "Start Date", "End Date", "Comments" }))
            {
                try
                {
                    _db.SaveChanges();

                    return RedirectToAction("ViewPrescriptions");
                }
                catch (RetryLimitExceededException /* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }

            }
            return View(prescriptionToUpdate);
        }

       

    }
}
