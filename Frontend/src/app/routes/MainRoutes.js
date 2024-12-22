import React from "react";
import { BrowserRouter, Route, Routes } from "react-router-dom";
import Home from "../pages/Admin/Home/Home";
import PageNotFound from "../layouts/adminLayout/PageNotFound";
import Header from "../layouts/adminLayout/Header/index";
import Footer from "../layouts/adminLayout/Footer/Footer";
import Navigation from "../layouts/adminLayout/Navigation";

export default function MainRoutes() {
  return (
    <BrowserRouter>
      {/* <Header /> */}
      <Navigation />
      <Routes>
        <Route path="/" element={<Home />} />
        <Route path="*" element={<PageNotFound />} />
      </Routes>
      <Footer />
    </BrowserRouter>
  );
}
