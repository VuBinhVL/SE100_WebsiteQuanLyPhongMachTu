import React from "react";
import PropTypes from "prop-types";
import "./DiseaseCard.css";

export default function ServiceCard({ img, title, description }) {
  return (
    <div className="service-card">
      <div className="service-card-img">
        <img src={img} alt={title} />
      </div>
      <h3 className="service-card-title">{title}</h3>
      <p className="service-card-description">{description}</p>
    </div>
  );
}

ServiceCard.propTypes = {
  img: PropTypes.string.isRequired, // Đường dẫn img
  title: PropTypes.string.isRequired, // Tiêu đề
  description: PropTypes.string.isRequired, // Mô tả
};
