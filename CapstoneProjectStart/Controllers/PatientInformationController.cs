using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CapstoneProjectStart.Models;
using System.Net;

namespace CapstoneProjectStart.Controllers
{
    public class PatientInformationController : Controller
    {
        // GET: PatientInformation
        private PatientsDBEntities3 _db = new PatientsDBEntities3();
        public ActionResult Index()
        {
            
            return View(_db.Appointments.ToList());
        }

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

        // GET: PatientInformation/Edit/5
       /* [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditAppointments(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appointment appointmentToEdit = await _db.Appointments.Findsync(id);
            if (accountCategory == null)
            {
                return HttpNotFound();
            }
            return View(accountCategory);
        }*/

        
        
        
        // POST: PatientInformation/Edit/5
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(Appointment appointmentToEdit)
        {
            var originalPatient = (from m in _db.Appointments

                                 where m.Patient_ID == appointmentToEdit.Patient_ID

                                 select m).First();
            if (!ModelState.IsValid)

                return View(appointmentToEdit);

           

            _db.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: PatientInformation/Delete/5
        
    }
}
