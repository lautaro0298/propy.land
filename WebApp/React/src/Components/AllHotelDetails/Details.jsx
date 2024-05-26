import React, { useState } from 'react'
import "./Details.css";
import Overview from './Overview';
import Photos from './Photos';

const HotelDetails = () => {
  const [tab, setTab] = useState('overview');

  const handleTabClick = (e) => {
    const newTab = e.target.value;
    setTab(prevTab => (prevTab === newTab ? 'overview' : newTab));
  }

  let component;
  switch (tab) {
    case 'overview':
      component = <Overview key="overview" />;
      break;
    case 'photos':
      component = <Photos key="photos" />;
      break;
    default:
      component = null;
  }

  return (
    <div className="b-hd-parent-container-div">
      <div className="b-all-headings-div">
        <span><button value="overview" onClick={handleTabClick}>Overview</button></span>
        <span><button value="info">Info</button></span>
        <span><button value="photos" onClick={handleTabClick}>Photos</button></span>
        <span><button value="review">Review</button></span>
        <span><button value="deals">Deals</button></span>
      </div>
      <hr/>
      {component}
    </div>
  )
}

export default HotelDetails
