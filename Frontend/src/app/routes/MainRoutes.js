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

export default function MainRoutes() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<LayoutAdmin />}>
          <Route path="/" element={<DashBoard />} />
          <Route path="Staff" element={<Staff />} />
          <Route path="Patien" element={<Patien />} />
          <Route path="ExaminationForm" element={<ExaminationForm />} />
          <Route path="MedicalShift" element={<MedicalShift />} />
          <Route path="Medicine" element={<Medicine />} />
          <Route path="*" element={<PageNotFound />} />
        </Route>
      </Routes>
    </BrowserRouter>
  );
}
