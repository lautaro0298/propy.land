import React, { useState } from "react";
import "./Searchbar.css";

import "react-datepicker/dist/react-datepicker.css";
import DatePicker from "react-datepicker";
//importacion de el otro proyecto
import GuestCard from "../material-ui-components/GuestCard";
import addgroup from "../../Logos/addgroup.png";
import { useEffect } from "react";
import { useContext } from "react";
import { Link } from "react-router-dom";
import styled from "styled-components";

const Searchbar = () => {
    // variables a importar del 2 proyecto
    const { searchData, handleSearchData, handleGuestsData, guestsData, handleRoomsData, handleDays, handleFirstDate, handleSecondDate } = useContext(SearchDataContext)
    console.log(searchData);
    const [show, setShow] = useState(false);
    const [guestSelect, setGuestSelect] = useState(false);
    const [guestNumber, setGuestNumber] = useState(2);
    const [roomsNumber, setRoomsNumber] = useState(1);
    //mas importaciones
    useEffect(() => {
        setShow(true);
    }, []);

    const handleSearchButton = () => {

        handleSearchData(location)
        handleGuestsData(guestNumber)
        handleRoomsData(roomsNumber)

        handleFirstDate(checkInDate)
        handleSecondDate(checkOutDate)

        let first = Number(checkInDate.slice(8, 10))
        let last = Number(checkOutDate.slice(8, 10))

        let fmonth = checkInDate.slice(4, 7)
        let smonth = checkOutDate.slice(4, 7)

        if (fmonth === smonth) {
            handleDays(last - first)
        } else {
            if (smonth === "Jan" || smonth === "Mar" || smonth === "May" || smonth === "Jul" || smonth === "Aug" || smonth === "Oct" || smonth === "Nov" || smonth === "Dec") {
                if (last < first) {
                    if (fmonth === "Jun" || fmonth === "Sep" || fmonth === "Nov" || fmonth === "Apr") {
                        handleDays(30 - first + last)
                    } else if (fmonth === "Jan" || fmonth === "Mar" || fmonth === "May" || fmonth === "Jul" || fmonth === "Aug" || fmonth === "Oct" || fmonth === "Nov" || fmonth === "Dec") {
                        handleDays(31 - first + last)
                    }
                }
            }
            if (smonth === "Jun" || smonth === "Sep" || smonth === "Nov" || smonth === "Apr") {
                if (last < first) {
                    if (fmonth === "Jun" || fmonth === "Sep" || fmonth === "Nov" || fmonth === "Apr") {
                        handleDays(30 - first + last)
                    } else if (fmonth === "Jan" || fmonth === "Mar" || fmonth === "May" || fmonth === "Jul" || fmonth === "Aug" || fmonth === "Oct" || fmonth === "Nov" || fmonth === "Dec") {
                        handleDays(31 - first + last)
                    }
                }
            }
        }
    }

   

 { guestSelect && <GuestCard top="36rem" position="absolute" right="30rem" setGuestNumber={setGuestNumber} setRoomsNumber={setRoomsNumber} /> }

    const handleGuestSelector = () => {
        setGuestSelect(!guestSelect)
    }
  const [searchText, setsearchText] = useState("");
  const [checkInDate, setCheckInDate] = useState(new Date());
  const [checkOutDate, setCheckOutDate] = useState(new Date());
  console.log(searchText);
    return (

    <div className="searchbar-holder">
      <div className="row">
        <div className="test">
          <div>
            <div className="wrapper_1">
              <div className="all">All Stays</div>
              <div className=" all hotel">Hotel</div>
              <div className="all appr">Hotel/Appartment</div>
            </div>
          </div>
          <div>
            <div>
              <div className="wrapper_2">
                <div className="inp-holder-1">
                  <div className="icon">
                    <img
                      src="https://cdn-icons-png.flaticon.com/512/67/67347.png"
                      alt="map-pointer"
                    />
                  </div>
                  <div className="inputbox-1">
                    <input
                      type="text"
                      placeholder="Enter a hotel name or destination"
                      value={searchText}
                      onChange={(e) => setsearchText(e.target.value)}
                    />
                  </div>
                  <div className="buttonbox">
                    <button onClick={() => console.log("Hello There")}>
                      &times;
                    </button>
                  </div>
                </div>
                <div className="inp-holder-2">
                  <div className="icon">
                    <img
                      src="https://cdn-icons-png.flaticon.com/128/2948/2948088.png"
                      alt="map-pointer"
                    />
                  </div>
                  <div className="datepicker-holder">
                    <div className="datepicker-1">
                      <div className="ci">Check In</div>
                      <div>
                        <DatePicker
                          selected={checkInDate}
                          onChange={(date) => setCheckInDate(date)}
                        />
                      </div>
                    </div>
                    <div className="datepicker-2">
                      <div className="co">Check Out</div>
                      <div>
                        <DatePicker
                          selected={checkOutDate}
                          onChange={(date) => setCheckOutDate(date)}
                        />
                      </div>
                    </div>
                  </div>
                </div>
              
                                <SelectGuestsWrapper>
                                    <div>
                                        <div onClick={handleGuestSelector} className="guestsnumber">
                                            <img src={addgroup} alt="" />
                                            <div className="guest-al">
                                                <span>{roomsNumber} Room</span>
                                                <span>{guestNumber} Guests</span>
                                            </div>
                                        </div>
                                        <button onClick={handleSearchButton}>Search</button>
                                    </div>
                                </SelectGuestsWrapper>
                           
                </div>
              </div>
            </div>
          </div>
          <div>
            <div className="wrapper_3">
              <div className="heading">
                <h5>We compare multiple propys site once</h5>
              </div>
                      
                        
            
            </div>
          </div>
        </div>
      </div>
    
  );
};
export const SelectGuestsWrapper = styled.div`
  width: 32%;
  /* padding: 1rem; */
  padding-left: 1rem;
  & > div {
    display: flex;
    align-items: center;
    height: 100%;
    img {
      width: 18px;
      height: 18px;
      position: relative;
      top: 4px;
    }
    .guestsnumber {
      display: flex;
      width: 50%;
      :active {
        border: 1px dotted;
      }
      .guest-al {
        display: grid;
        position: relative;
        left: 1rem;
        & > span:nth-child(1) {
          font-size: 11px;
        }
        & > span:nth-child(2) {
          font-size: 12px;
          font-weight: bold;
        }
      }
    }
    button {
    width: 60%;
    height:80%;
    padding: 1rem;
    background-color: #007fad;
    border: 1px solid #007fad;
    border-bottom-color: #005f81;
    border-radius: 4rem;
    color: white;
    outline: none;
    border: none;
    font-size: 16px;
    font-weight: 700;
      :hover {
        background-color: #005f81;
        cursor: pointer;
      }
    }
  }
`;
export { Searchbar };
