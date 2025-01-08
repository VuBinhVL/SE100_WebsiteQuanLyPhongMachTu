import React from "react";
import { BrowserRouter, Route, Routes } from "react-router-dom";
import PageNotFound from "../layouts/adminLayout/PageNotFound";
import Forbidden from "../layouts/adminLayout/Forbidden";
import LayoutAdmin from "../layouts/adminLayout";
import Staff from "../pages/Admin/UsersManagement/Staffs";
import DashBoard from "../pages/Admin/DashBoard";
import Patien from "../pages/Admin/UsersManagement/Patiens";
import ExaminationForm from "../pages/Admin/ExaminationFormsManagement";
import MedicalShift from "../pages/Admin/MedicalShiftsManagement";
import CustomerHome from "../pages/Customer/CustomerHome";
import Doctor from "../pages/Customer/Doctor";
import Register from "../pages/Other/Register/Register";
import Login from "../pages/Other/Login";
import LayoutCustomer from "../layouts/customerLayout";
import ForgetPassword from "../pages/Other/ForgetPassword";
import AccountInformation from "../pages/Customer/Account/AccountInformation";
import UserRecordList from "../pages/Customer/MedicalRecord/UserRecordList";
import DetailRecord from "../pages/Customer/MedicalRecord/DetailRecord";
import ReviewPiceList from "../pages/Customer/Service/ReviewPriceList";
import MedicalExamList from "../pages/Customer/Service/MedicalExamList";
import InformationManagement from "../pages/Admin/InformationManagement/InformationManagement";
import Parameter from "../pages/Admin/ParameterManagement";
import DiseaseGroup from "../pages/Admin/DiseaseGroupManagement";
import MedicineGroup from "../pages/Admin/MedicineGroupManagement";
import MedicineManagement from "../pages/Admin/MedicinesManagement/MedicineManagement";
import LayoutStaff from "../layouts/staffLayout";
import Patient from "../pages/Staff/PatienManagement";
import MedicalshiftOfStaff from "../pages/Staff/MedicalShiftManagement/MedicalShifts";
import Appointment from "../pages/Staff/MedicalShiftManagement/Appointment";

export default function MainRoutes() {
  return (
    <BrowserRouter>
      <Routes>
        {/* Layout khách hàng */}
        <Route path="/" element={<LayoutCustomer />}>
          <Route index element={<CustomerHome />} />
          <Route path="review-price-list" element={<ReviewPiceList />} />
          <Route path="medical-exam-list" element={<MedicalExamList />} />
          <Route path="register" element={<Register />} />
          <Route path="login" element={<Login />} />
          <Route path="forget-password" element={<ForgetPassword />} />
          <Route path="doctors" element={<Doctor />} />
          <Route path="account" element={<AccountInformation />} />
          <Route path="medical-record" element={<UserRecordList />} />
          <Route path="/detail-record/:id" element={<DetailRecord />} />{" "}
          {/* Định tuyến với id */}
        </Route>

        {/* Layout quản trị viên */}
        <Route path="/admin" element={<LayoutAdmin />}>
          <Route index element={<DashBoard />} />
          <Route path="staff" element={<Staff />} />
          <Route path="patien" element={<Patien />} />
          <Route path="examinationform" element={<ExaminationForm />} />
          <Route path="medicalshift" element={<MedicalShift />} />
          <Route path="medicine" element={<MedicineManagement />} />
          <Route
            path="information-management"
            element={<InformationManagement />}
          />
          <Route path="medicine-group" element={<MedicineGroup />} />
          <Route path="disease-group" element={<DiseaseGroup />} />
          <Route path="parameter" element={<Parameter />} />
        </Route>
        {/* Layout Nhân viên */}
        <Route path="/Staff" element={<LayoutStaff />}>
          <Route index element={<Patient />} />
          <Route path="appointment" element={<Appointment />} />
          <Route path="medicalshiftOfStaff" element={<MedicalshiftOfStaff />} />
          <Route
            path="information-management"
            element={<InformationManagement />}
          />
        </Route>
        {/* Route cho trang Forbidden*/}
        <Route path="/forbidden" element={<Forbidden />} />
        {/* Trang không tìm thấy */}
        <Route path="*" element={<PageNotFound />} />
      </Routes>
    </BrowserRouter>
  );
}
