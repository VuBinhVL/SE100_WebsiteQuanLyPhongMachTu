import React, { useState, useMemo } from 'react';
import { Card, Col, DatePicker, Row, Space, Statistic } from 'antd';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faHospitalUser, faMoneyCheckDollar, faPills, faUser } from '@fortawesome/free-solid-svg-icons';
import dayjs from 'dayjs';
import LineChart from './LineChart';
import CountUp from 'react-countup';
import PieChart from './PieChart';
import BarChart from './BarChart'; 
import "./DashBoard.css"

const { RangePicker } = DatePicker;

export default function DashBoard() {
  // Dữ liệu mẫu tĩnh
  const staticDashboardData = {
    total_revenue: 500000000,
    total_medicine: 120,
    total_patient: 1500,
    total_ticket: 1800,
    new_patient: 50,
    new_medicine: 10,
    new_revenue: 12000000,
    new_ticket: 100,
    patient_data: [
      { date: '2024-12-22', value: 10 },
      { date: '2024-12-23', value: 20 },
      { date: '2024-12-24', value: 30 },
    ],
    revenue_data: [
      { date: '2024-12-22', value: 1000000 },
      { date: '2024-12-23', value: 1500000 },
      { date: '2024-12-24', value: 2000000 },
    ],
    medicine_usage: [
      { name: 'Thuốc A', value: 30 },
      { name: 'Thuốc B', value: 20 },
      { name: 'Thuốc C', value: 50 },
    ],
    disease_data: [
      { disease: 'Bệnh tim', count: 300 },
      { disease: 'Tiểu đường', count: 200 },
      { disease: 'Huyết áp cao', count: 400 },
      { disease: 'Cảm cúm', count: 600 },
    ],
  };

  const [dashboard] = useState(staticDashboardData);
  const [dateRange, setDateRange] = useState([dayjs(new Date()).subtract(7, 'day'), dayjs(new Date())]);

  const onChange = (value) => {
    setDateRange(value);
  };

  const totalValueMetric = useMemo(
     () => [
       {
         label: 'Tổng doanh thu',
         icon: faMoneyCheckDollar,
         color: '#3F8600',
         value: dashboard.total_revenue,
       },
       {
         label: 'Tổng số thuốc',
         icon: faPills,
         color: '#3F8600',
         value: dashboard.total_medicine,
       },
       {
         label: 'Tổng số bệnh nhân',
         icon: faHospitalUser,
         color: '#9847FF',
         value: dashboard.total_patient,
       },
       {
         label: 'Tổng số lượt khám',
         icon: faUser,
         color: '#CF1322',
         value: dashboard.total_ticket,
       },
     ],
     [dashboard]
   );
 
   const newValueMetric = useMemo(
     () => [
       {
         label: 'Bệnh nhân mới',
         color: '#3f8600',
         value: dashboard.new_patient,
       },
       {
         label: 'Lượng thuốc',
         color: '#3f8600',
         value: dashboard.new_medicine,
       },
       {
         label: 'Doanh thu',
         color: '#3f8600',
         value: dashboard.new_revenue,
       },
       {
         label: 'Lượt khám',
         color: '#3f8600',
         value: dashboard.new_ticket,
       },
     ],
     [dashboard]
   );
 

  const formatter = (value) => <CountUp end={value} separator=" " />;

  return (
    <div>
      <div className="my-5">
        <Row className="p-5">
          <Space direction="horizontal" size={12}>
            <span>Chọn ngày</span>
            <RangePicker format="DD/MM/YYYY" value={dateRange} onChange={onChange} />
          </Space>
        </Row>
        <div className="flex flex-col gap-5">
          {/* Tổng các chỉ số - Now in one row */}
          <Row gutter={16}>
            {totalValueMetric.map((metric, i) => (
              <Col key={i} span={6}>
                <Card
                                  bordered={false}
                                  className="flex flex-col items-center justify-center"
                                >
                                  <span className="text-[20px] font-medium">{metric.label}</span>
                                  <Statistic
                                    formatter={formatter}
                                    className="pt-3"
                                    value={metric.value}
                                    valueStyle={{ color: metric.color }}
                                    prefix={<FontAwesomeIcon icon={metric.icon} />}
                                  />
                                </Card>
              </Col>
            ))}
          </Row>

          {/* Tổng quan */}
          <Card bordered={false} className="w-full">
            <span className="text-[20px] font-medium">Tổng quan</span>
            <Row gutter={16} className="mt-5">
              {newValueMetric.map((metric, i) => (
                <Col key={i} span={12} className="mb-5">
                  <Card bordered={false}>
                    <Statistic
                      formatter={formatter}
                      title={metric.label}
                      value={metric.value}
                      valueStyle={{ color: metric.color }}
                    />
                  </Card>
                </Col>
              ))}
            </Row>
          </Card>

          {/* Biểu đồ */}
          <div className="w-full flex flex-row gap-5">
            {/* Biểu đồ doanh thu */}
            <Card bordered={false} className="w-full">
              <p className="mb-5 text-[20px] flex items-center justify-center font-medium">
                Biểu đồ doanh thu
              </p>
              <LineChart
                data={dashboard.revenue_data}
                startDate={dateRange[0]}
                endDate={dateRange[1]}
                color="#00855f"
              />
            </Card>
            {/* Biểu đồ tròn cho thuốc */}
            <Card bordered={false} className="w-full">
              <p className="mb-5 text-[20px] flex items-center justify-center font-medium">
                Tỷ lệ sử dụng thuốc
              </p>
              <PieChart 
                data={dashboard.medicine_usage} 
                colors={['#FF6384', '#36A2EB', '#FFCE56']} 
              />
            </Card>
          </div>

          {/* Biểu đồ bệnh lý */}
          <Card bordered={false} className="w-full">
            <p className="mb-5 text-[20px] flex items-center justify-center font-medium">
              Số lượng bệnh nhân theo bệnh lý
            </p>
            <BarChart 
              data={dashboard.disease_data}
              color="#4CAF50"
            />
          </Card>
        </div>
      </div>
    </div>
  );
}