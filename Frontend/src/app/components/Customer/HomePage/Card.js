import React from "react";
import PropTypes from "prop-types";
import "./Card.css";

export default function Card({ img, title, description }) {
  return (
    <div className="item_card_serivce">
      <div className="card-img">
        <img src={img} alt={title} />
      </div>
      <h3 className="card-title">{title}</h3>
      <p className="card-description">{description}</p>
    </div>
  );
}

// Định nghĩa kiểu dữ liệu cho props
Card.propTypes = {
  img: PropTypes.string.isRequired, // URL hình ảnh img
  title: PropTypes.string.isRequired, // Tiêu đề
  description: PropTypes.string.isRequired, // Mô tả
};
