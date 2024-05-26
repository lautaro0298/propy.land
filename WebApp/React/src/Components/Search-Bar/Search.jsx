import React, { useState } from 'react';
import PropTypes from 'prop-types';
import styled from 'styled-components';
import DateRangePicker from 'react-date-range';
import './Search.css';

const SearchWrapper = styled.div`
  position: ${(props) => props.position};
  top: ${(props) => props.top};
  left: ${(props) => props.left};
  width: 46vw;
  background-color: white;
  box-shadow: 1px 7px 27px -3px black;
  animation: 0.8s AnimateRight 0s forwards;
  transform: translateX(-30%);
  font-size: 14px;
  z-index: 1;
`;

export function Search({ setCheckOutDate, setCheckInDate, top, left, position }) {
  const [startDate, setStartDate] = useState(new Date());
  const [endDate, setEndDate] = useState(new Date());

  const selectionRange = {
    startDate,
    endDate,
    key: 'selection',
  };

  function handleSelect(ranges) {
    setStartDate(ranges.selection.startDate);
    setEndDate(ranges.selection.endDate);

    try {
      setCheckInDate(ranges.selection.startDate.toISOString().slice(0, 10));
      setCheckOutDate(ranges.selection.endDate.toISOString().slice(0, 10));
    } catch (error) {
      console.error(error);
    }

    if (endDate < startDate) {
      setEndDate(startDate);
    }
  }

  return (
    <SearchWrapper top={top} position={position} left={left}>
      <DateRangePicker
        minDate={new Date()}
        ranges={[selectionRange]}
        onChange={handleSelect}
        key="selection"
      />
    </SearchWrapper>
  );
}

Search.propTypes = {
  setCheckOutDate: PropTypes.func.isRequired,
  setCheckInDate: PropTypes.func.isRequired,
  top: PropTypes.string.isRequired,
  left: PropTypes.string.isRequired,
  position: PropTypes.string.isRequired,
};
