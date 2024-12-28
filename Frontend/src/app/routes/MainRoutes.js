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



export default function MainRoutes() {
  return (
    <BrowserRouter>
      <Routes>
        {/* Layout khách hàng */}
        <Route path="/" element={<LayoutCustomer />}>
          <Route index element={<CustomerHome />} />
          <Route path="register" element={<Register />} />
          <Route path="login" element={<Login />} />
          <Route path="doctors" element={<Doctor />} />
        </Route>

        {/* Layout quản trị viên */}
        <Route path="/admin" element={<LayoutAdmin />}>
          <Route index element={<DashBoard />} />
          <Route path="staff" element={<Staff />} />
          <Route path="patien" element={<Patien />} />
          <Route path="examinationform" element={<ExaminationForm />} />
          <Route path="medicalshift" element={<MedicalShift />} />
          <Route path="medicine" element={<Medicine />} />
        </Route>
        {/* Trang không tìm thấy */}
        <Route path="*" element={<PageNotFound />} />
      </Routes>
    </BrowserRouter>
  );
}
