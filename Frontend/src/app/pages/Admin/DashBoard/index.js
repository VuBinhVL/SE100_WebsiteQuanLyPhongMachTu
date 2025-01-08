import React, { useState, useMemo, useEffect, useCallback } from 'react';
import { Card, Col, DatePicker, Row, Space, Statistic } from 'antd';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faHospitalUser, faMoneyCheckDollar, faPills, faUser, faCalendarAlt } from '@fortawesome/free-solid-svg-icons';
import dayjs from 'dayjs';
import LineChart from './LineChart';
import CountUp from 'react-countup';
import PieChart from './PieChart';
import BarChart from './BarChart';
import "./DashBoard.css"
import {fetchGet} from "../../../lib/httpHandler";

const { RangePicker } = DatePicker;

export default function DashBoard() {

  const [dateRange, setDateRange] = useState([dayjs(new Date()).subtract(7, 'day'), dayjs(new Date())]);

  const onChange = useCallback((value) => {
    if (value && value.length === 2) {
      setDateRange(value);
    }
  }, []);

 

  const [listThongSo,setListThongso] = useState([]);

  //Lấy danh sách thông số ở row 1  
  useEffect(() => {

    const uri = "/api/admin/dashboard/thong-ke";
    const startDay = dateRange[0].format('YYYY-MM-DD');
    const endDay = dateRange[1].format('YYYY-MM-DD');
    
    fetchGet(
      `${uri}?startDay=${startDay}&endDay=${endDay}`,
      (sus) => {
        setListThongso(sus);
        console.log(listThongSo);
      },
      (fail) => {
        alert(fail.message);
      },
      () => {
        alert("Có lỗi xảy ra");
      }
    );
  }, [dateRange]); // Change dependency to dateRange and listThongSo to refetch when dates change
  const [revenueData, setRevenueData] = useState([]);
  useEffect(() => {
    const uri = "/api/admin/dashboard/bieu-do-doanh-thu";
    const startDay = dateRange[0].format('YYYY-MM-DD');
    const endDay = dateRange[1].format('YYYY-MM-DD');
    
    fetchGet(
      `${uri}?startDay=${startDay}&endDay=${endDay}`,
      (sus) => {
        setRevenueData(sus);
        console.log(revenueData);
      },
      (fail) => {
        alert(fail.message);
      },
      () => {
        alert("Có lỗi xảy ra khi lấy dữ liệu biểu đồ doanh thu");
      }
    );
  }, [dateRange]); // Gọi lại khi dateRange thay đổi

  const [medicineUsageData, setMedicineUsageData] = useState([]);
  useEffect(() => {
    const uri = "/api/admin/dashboard/bieu-do-thuoc";
    const startDay = dateRange[0].format('YYYY-MM-DD');
    const endDay = dateRange[1].format('YYYY-MM-DD');
    
    fetchGet(
      `${uri}?startDay=${startDay}&endDay=${endDay}`,
      (sus) => {
        setMedicineUsageData(sus);
        
      },
      (fail) => {
        alert(fail.message);
      },
      () => {
        alert("Có lỗi xảy ra khi lấy dữ liệu biểu đồ thuốc");
      }
    );
  }, [dateRange]);
  console.log(medicineUsageData);

  const [diseaseData, setDiseaseData] = useState([]);

  useEffect(() => {
    const uri = "/api/admin/dashboard/bieu-do-benh-nhan-theo-benh-ly";
    const startDay = dateRange[0].format('YYYY-MM-DD');
    const endDay = dateRange[1].format('YYYY-MM-DD');
    
    fetchGet(
      `${uri}?startDay=${startDay}&endDay=${endDay}`,
      (sus) => {
        setDiseaseData(sus);
        console.log(diseaseData); // Để kiểm tra dữ liệu
      },
      (fail) => {
        alert(fail.message);
      },
      () => {
        alert("Có lỗi xảy ra khi lấy dữ liệu biểu đồ bệnh lý");
      }
    );
  }, [dateRange]);
  console.log(diseaseData);
  const formatter = (value) => <CountUp end={value} separator=" " />;

  return (
    <div>
      <div className="mb-5">
        <Row className="my-3">
          <Space direction="horizontal" size={12}>
            <span>Chọn ngày</span>
            <RangePicker 
  format="DD/MM/YYYY" 
  value={dateRange} 
  onChange={onChange}
  suffixIcon={<FontAwesomeIcon icon={faCalendarAlt} onClick={(e) => e.preventDefault() || document.querySelector('.ant-picker-input input').focus()}/>}
/>
          </Space>
        </Row>

          {/* Tổng các chỉ số - Now in one row */}
          <Row gutter={16}>
  {Object.keys(listThongSo).length > 0 && [
    {
      label: 'Tổng doanh thu',
      icon: faMoneyCheckDollar,
      color: '#3F8600',
      value: listThongSo.tongDoanhThu, // Loại bỏ [0]
    },
    {
      label: 'Tổng số thuốc',
      icon: faPills,
      color: '#3F8600',
      value: listThongSo.tongSoThuoc, // Loại bỏ [0]
    },
    {
      label: 'Tổng số bệnh nhân',
      icon: faHospitalUser,
      color: '#9847FF',
      value: listThongSo.tongSoBenhNhan, // Loại bỏ [0]
    },
    {
      label: 'Tổng số lượt khám',
      icon: faUser,
      color: '#CF1322',
      value: listThongSo.tongSoLuotKham, // Loại bỏ [0]
    }
  ].map((metric, i) => (
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
        {/* Biểu đồ */}
<div className="w-full flex flex-row gap-5">
  {/* Biểu đồ doanh thu */}
  <Card bordered={false} className="w-full">
    <p className="mb-5 text-[20px] flex items-center justify-center font-medium">
      Biểu đồ doanh thu
    </p>
    <LineChart
      data={revenueData}
      startDate={dateRange[0]}
      endDate={dateRange[1]}
      color="#00855f"
    />
  </Card>
</div>
{/* Biểu đồ tròn cho thuốc */}
<Card bordered={false} className="w-full">
              <p className="mb-5 text-[20px] flex items-center justify-center font-medium">
                Tỷ lệ sử dụng thuốc
              </p>
              <PieChart
                data={medicineUsageData}
                colors={['#FF6384', '#36A2EB', '#FFCE56']}
              />
            </Card>
      {/* Biểu đồ bệnh lý */}
      <Card bordered={false} className="w-full">
            <p className="mb-5 text-[20px] flex items-center justify-center font-medium">
              Số lượng bệnh nhân theo bệnh lý
            </p>
            <BarChart
              data={diseaseData}
              color="#4CAF50"
            />
          </Card>
      </div>
    </div>
  );
}