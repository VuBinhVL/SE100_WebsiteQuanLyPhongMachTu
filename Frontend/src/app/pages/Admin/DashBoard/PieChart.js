// piechart.js
import React from 'react';
import { PieChart as RePieChart, Pie, Cell, Legend, Tooltip } from 'recharts';

const COLORS = ['#0088FE', '#00C49F', '#FFBB28', '#FF8042', '#AF19FF'];

const PieChart = ({ data, colors }) => {
  return (
    <RePieChart width={400} height={400}>
      <Pie
        data={data}
        dataKey="phanTram" // Sử dụng phanTram như dataKey
        nameKey="tenThuoc" // Sử dụng tenThuoc như nameKey
        cx="50%"
        cy="50%"
        outerRadius={150}
        fill="#8884d8"
        label
      >
        {data.map((entry, index) => (
          <Cell key={`cell-${index}`} fill={colors[index % colors.length]} />
        ))}
      </Pie>
      <Tooltip />
      <Legend />
    </RePieChart>
  );
};

export default PieChart;