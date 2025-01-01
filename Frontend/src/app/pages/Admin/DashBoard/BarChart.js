import React from 'react';
import { BarChart as ReBarChart, Bar, XAxis, YAxis, Tooltip, Legend } from 'recharts';

const BarChart = ({ data, color }) => {
  return (
    <ReBarChart
      width={500}
      height={300}
      data={data}
      margin={{
        top: 5,
        right: 30,
        left: 20,
        bottom: 5,
      }}
    >
      <XAxis dataKey="disease" />
      <YAxis />
      <Tooltip />
      <Legend />
      <Bar dataKey="count" fill={color || '#8884d8'} />
    </ReBarChart>
  );
};

export default BarChart;