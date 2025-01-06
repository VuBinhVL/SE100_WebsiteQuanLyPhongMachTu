import React from "react";
import { BrowserRouter, Route, Routes } from "react-router-dom";
import PageNotFound from "../layouts/adminLayout/PageNotFound";
import LayoutAdmin from "../layouts/adminLayout";
import Staff from "../pages/Admin/UsersManagement/Staffs";
import DashBoard from "../pages/Admin/DashBoard";
import Patien from "../pages/Admin/UsersManagement/Patiens";
import ExaminationForm from "../pages/Admin/ExaminationFormsManagement";
import MedicalShift from "../pages/Admin/MedicalShiftsManagement";
import Medicine from "../pages/Admin/MedicinesManagement";
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
          <Route path="medical" element={<DetailRecord />} />
        </Route>

        {/* Layout quản trị viên */}
        <Route path="/admin" element={<LayoutAdmin />}>
          <Route index element={<DashBoard />} />
          <Route path="staff" element={<Staff />} />
          <Route path="patien" element={<Patien />} />
          <Route path="examinationform" element={<ExaminationForm />} />
          <Route path="medicalshift" element={<MedicalShift />} />
          <Route path="medicine" element={<Medicine />} />
          <Route
            path="information-management"
            element={<InformationManagement />}
          />
        </Route>
        {/* Trang không tìm thấy */}
        <Route path="*" element={<PageNotFound />} />
      </Routes>
    </BrowserRouter>
  );
}
