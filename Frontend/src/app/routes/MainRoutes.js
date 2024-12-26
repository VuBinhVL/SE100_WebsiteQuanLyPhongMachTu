import React from "react";
import { BrowserRouter, Route, Routes } from "react-router-dom";
import PageNotFound from "../layouts/adminLayout/PageNotFound";
import CustomerFooter from "../layouts/customerLayout/Footer";
import CustomerHeader from "../layouts/customerLayout/Header/CustomerHeader";
import CustomerHome from "../pages/Customer/CustomerHome";
import Doctor from "../pages/Customer/Doctor";

export default function MainRoutes() {
  return (
    <BrowserRouter>
      <CustomerHeader />
      <Routes>
        <Route path="/" element={<CustomerHome />} />
        <Route path="*" element={<PageNotFound />} />
        <Route path="/doctors" element={<Doctor />} />
      </Routes>
      <CustomerFooter />
    </BrowserRouter>
  );
}
