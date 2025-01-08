import 'chart.js/auto';
import dayjs from 'dayjs';
import React from 'react';
import { Line } from 'react-chartjs-2';

// Trong LineChart.js
function getMonthLabels(startDate, endDate) {
  let labels = [];
  let currentDate = startDate.clone().startOf('month');
  while (currentDate.format('YYYYMM') <= endDate.format('YYYYMM')) {
    labels.push(`${currentDate.format('MM/YYYY')}`);
    currentDate = currentDate.add(1, 'month');
  }
  return labels;
}

function mapDataToLabels(data, labels) {
  let dataPoints = new Array(labels.length).fill(0); // Khởi tạo mảng với các giá trị 0
  data.forEach(item => {
    let label = `${item.thang.toString().padStart(2, '0')}/${item.nam}`;
    let index = labels.indexOf(label);
    if (index !== -1) {
      dataPoints[index] = item.tongDoanhThu;
    }
  });
  return dataPoints;
}

export default function LineChart({ data, startDate, endDate, color }) {
  const labels = getMonthLabels(startDate, endDate);
  const dataPoints = mapDataToLabels(data, labels); // Chuyển đổi dữ liệu

  const chartData = {
    labels: labels,
    datasets: [
      {
        backgroundColor: color,
        borderColor: color,
        data: dataPoints,
      },
    ],
  };

  const options = {
    responsive: true,
    plugins: {
      legend: {
        display: false,
      },
    },
  };

  return <Line data={chartData} options={options} />;
}