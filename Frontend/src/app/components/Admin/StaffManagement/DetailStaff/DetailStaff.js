import React from "react";
import { GrCircleInformation } from "react-icons/gr";
import { IoIosArrowDown } from "react-icons/io";
import { useState } from "react"
import { TiEdit } from "react-icons/ti";
import "./DetailStaff.css"
export default function DetailStaff() {
    const [imageSrc, setImageSrc] = useState("https://www.shutterstock.com/image-photo/happy-female-doctor-stethoscope-on-600nw-2527451925.jpg");
    const [editStatus, setEditStatus] = useState(false);
    const handleSlectImage = () => {
        const fileInput = document.getElementById("fileInput")
        fileInput.click();
    }
    const handleFileChange = (event) => {
        const file = event.target.files[0]; // Lấy file người dùng chọn
        if (file) {
            const reader = new FileReader(); // Sử dụng FileReader để đọc file
            reader.onload = (e) => {
                setImageSrc(e.target.result); // Cập nhật URL ảnh
            };
            reader.readAsDataURL(file); // Đọc file dưới dạng Data URL
        }
    };
    const handleEditInformation = () => {
        setEditStatus(!editStatus);
    }
    return (
        <>
            {/* <!-- Button trigger modal --> */}
            <a href="#">
                <GrCircleInformation className="icon_information icon_action" data-bs-toggle="modal" data-bs-target="#detailStaff" />
            </a>
            {/* <!-- Modal --> */}
            <div className="modal fade" id="detailStaff" data-bs-backdrop="static" data-bs-keyboard="false" tabIndex="-1" aria-labelledby="staticBackdropLabel" aria-hidden="true">
                <div className="modal-dialog modal-lg">
                    <div className="modal-content">
                        <div className="modal-header">
                            <h5 className="modal-title fs-4" id="staticBackdropLabel">Detail staff</h5>
                            <button type="button" className="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                        </div>
                        {
                            editStatus ? (
                                <>
                                    <div className="modal-body d-flex justify-content-center">
                                        <form className="me-5 w-75">
                                            <div className="form-group mb-3 d-flex align-items-center">
                                                <label htmlFor="fullName" className="form-label col-4 custom-bold">Full Name:</label>
                                                <input className="form-control rounded-3" name="fullName" id="fullName" type="text" placeholder="Enter full name" />
                                            </div>
                                            <div className="form-group mb-3 d-flex align-items-center position-relative">
                                                <label htmlFor="gender" className="form-label col-4 custom-bold">Gender:</label>
                                                <select id="gender" name="gender" className="form-control rounded-3 ">
                                                    <option hidden disabled selected>Enter your gender</option>
                                                    <option value="male">Male</option>
                                                    <option value="female">Female</option>
                                                </select>
                                                <IoIosArrowDown className="position-absolute end-0 me-3" />
                                            </div>
                                            <div className="form-group mb-3 d-flex align-items-center">
                                                <label htmlFor="birthday" className="form-label col-4 custom-bold">Date of birth:</label>
                                                <input type="date" id="birthday" name="birthday" className="form-control rounded-3" />
                                            </div>
                                            <div className="form-group mb-3 d-flex align-items-center position-relative">
                                                <label htmlFor="specialization" className="form-label col-4 custom-bold">Specialization:</label>
                                                <select id="specialization" name="specialization" className="form-control rounded-3">
                                                    <option hidden disabled selected>Enter your specialization</option>
                                                    <option value="doctor">Doctor</option>
                                                    <option value="nurse">Nurse</option>
                                                </select>
                                                <IoIosArrowDown className="position-absolute end-0 me-3" />
                                            </div>
                                            <div className="form-group mb-3 d-flex align-items-center">
                                                <label htmlFor="phoneNumber" className="form-label col-4 custom-bold">Phone number:</label>
                                                <input name="phoneNumber" id="phoneNumber" type="text" className="form-control rounded-3" placeholder="Enter your phone number" />
                                            </div>
                                            <div className="form-group mb-3 d-flex align-items-center">
                                                <label htmlFor="email" className="form-label col-4 custom-bold">Email:</label>
                                                <input name="email" id="email" type="email" className="form-control rounded-3" placeholder="Enter your Email" />
                                            </div>
                                            <div className="form-group mb-3 d-flex align-items-center">
                                                <label htmlFor="address" className="form-label col-4 custom-bold">Address:</label>
                                                <input name="address" id="address" type="text" className="form-control rounded-3" placeholder="Enter your address" />
                                            </div>
                                        </form>
                                        <div className="contain_img d-flex justify-content-center flex-column align-items-center">
                                            <img className="inner_img rounded-circle mb-3" src={imageSrc} />
                                            <button className="btn btn-primary btn_select_img" onClick={handleSlectImage}>Select image</button>
                                            <input
                                                id="fileInput"
                                                type="file"
                                                className="d-none"
                                                accept="image/*"
                                                onChange={handleFileChange}
                                            />
                                        </div>
                                    </div>
                                    <div className="contain_Edit d-flex align-items-center mb-3 ms-3 d-none">
                                        <h4 className="title_edit fs-6 mb-0 me-2">Edit employee information</h4>
                                        <button className="bg-white border-0 p-0" onClick={handleEditInformation}>
                                            <TiEdit className="fs-3 icon_edit_information" />
                                        </button>
                                    </div>
                                    <div className="modal-footer">
                                        <button type="button" className="btn btn-secondary btn_Cancel" data-bs-dismiss="modal">Cancel</button>
                                        <button type="button" className="btn btn-primary btn_Accept">Accept</button>
                                    </div>
                                </>
                            ) : (
                                <>
                                    <div className="modal-body d-flex justify-content-center">
                                        <form className="me-5 w-75">
                                            <div className="form-group mb-3 d-flex align-items-center">
                                                <label htmlFor="fullName" className="form-label col-4 custom-bold">Full Name:</label>
                                                <input className="form-control rounded-3" name="fullName" id="fullName" type="text" placeholder="Enter full name" />
                                            </div>
                                            <div className="form-group mb-3 d-flex align-items-center position-relative">
                                                <label htmlFor="gender" className="form-label col-4 custom-bold">Gender:</label>
                                                <select id="gender" name="gender" className="form-control rounded-3 ">
                                                    <option hidden disabled selected>Enter your gender</option>
                                                    <option value="male">Male</option>
                                                    <option value="female">Female</option>
                                                </select>
                                                <IoIosArrowDown className="position-absolute end-0 me-3" />
                                            </div>
                                            <div className="form-group mb-3 d-flex align-items-center">
                                                <label htmlFor="birthday" className="form-label col-4 custom-bold">Date of birth:</label>
                                                <input type="date" id="birthday" name="birthday" className="form-control rounded-3" />
                                            </div>
                                            <div className="form-group mb-3 d-flex align-items-center position-relative">
                                                <label htmlFor="specialization" className="form-label col-4 custom-bold">Specialization:</label>
                                                <select id="specialization" name="specialization" className="form-control rounded-3">
                                                    <option hidden disabled selected>Enter your specialization</option>
                                                    <option value="doctor">Doctor</option>
                                                    <option value="nurse">Nurse</option>
                                                </select>
                                                <IoIosArrowDown className="position-absolute end-0 me-3" />
                                            </div>
                                            <div className="form-group mb-3 d-flex align-items-center">
                                                <label htmlFor="phoneNumber" className="form-label col-4 custom-bold">Phone number:</label>
                                                <input name="phoneNumber" id="phoneNumber" type="text" className="form-control rounded-3" placeholder="Enter your phone number" />
                                            </div>
                                            <div className="form-group mb-3 d-flex align-items-center">
                                                <label htmlFor="email" className="form-label col-4 custom-bold">Email:</label>
                                                <input name="email" id="email" type="email" className="form-control rounded-3" placeholder="Enter your Email" />
                                            </div>
                                            <div className="form-group mb-3 d-flex align-items-center">
                                                <label htmlFor="address" className="form-label col-4 custom-bold">Address:</label>
                                                <input name="address" id="address" type="text" className="form-control rounded-3" placeholder="Enter your address" />
                                            </div>
                                        </form>
                                        <div className="contain_img d-flex justify-content-center flex-column align-items-center">
                                            <img className="inner_img rounded-circle mb-3" src={imageSrc} />
                                            <button className="btn btn-primary btn_select_img d-none" onClick={handleSlectImage}>Select image</button>
                                            <input
                                                id="fileInput"
                                                type="file"
                                                className="d-none"
                                                accept="image/*"
                                                onChange={handleFileChange}
                                            />
                                        </div>
                                    </div>
                                    <div className="contain_Edit d-flex align-items-center mb-3 ms-3">
                                        <h4 className="title_edit fs-6 mb-0 me-2">Edit employee information</h4>
                                        <button className="bg-white border-0 p-0" onClick={handleEditInformation}>
                                            <TiEdit className="fs-3 icon_edit_information" />
                                        </button>
                                    </div>
                                </>
                            )
                        }
                    </div>
                </div>
            </div>
        </>
    );
}