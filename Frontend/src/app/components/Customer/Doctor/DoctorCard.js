import React from "react";
import "./DoctorCard.css";

export default function DoctorCard({ image, name, details }) {
  return (
    <div className="doctor-card">
      <div className="doctor-image-container">
        <img src={image} alt={name} className="doctor-image" />
      </div>
      <div className="doctor-info">
        <h3 className="doctor-name">{name}</h3>
        <ul className="doctor-details">
          {details.map((detail, index) => (
            <li key={index}>{detail}</li>
          ))}
        </ul>
      </div>
    </div>
  );
}
