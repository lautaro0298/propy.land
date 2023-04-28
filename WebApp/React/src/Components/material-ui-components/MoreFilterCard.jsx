import React from "react";
import { makeStyles } from "@material-ui/core/styles";
import Card from "@material-ui/core/Card";
import { BtnsWrapper } from "./GuestCard";
import CardContent from "@material-ui/core/CardContent";
import styled from "styled-components";
import { priceFilter } from '../../store/actions';
import axios from "axios";
import { shallowEqual, useDispatch, useSelector } from "react-redux";
import { useState, useEffect } from "react";
import CloseIcon from '@material-ui/icons/Close'; // Agregar el icono de cierre



//////////filtering with stars

const useStyles = makeStyles({
    root: {
        
        overflow: "auto!important",
        fontSize: "14px",
        display: "inline-block",
        gridTemplateColumns: "1fr",
        gridGap: "1rem",
        margin: "4rem auto",
        boxShadow: "1px 1px 20px black",
        
        top: "15rem",
        right: "21rem"
    },
    starsCont: {
        display: "inline-block",
        gridTemplateColumns: "1fr",
        gridGap: "1rem",
    },
    facilitiesCont: {
        display: "grid",
        gridTemplateColumns: "1fr",
        gridGap: "1rem",
        boxShadow: " 0px 2px 1px -2px grey",
    },
    bullet: {
        display: "inline-block",
        margin: "0 2px",
        transform: "scale(0.8)",
    },
    title: {
        fontSize: 14,
    },
    pos: {
        marginBottom: 12,
    },
});



export function MoreFilterCard({ handleMoreFilterCard}) {
    const dispatch = useDispatch();
    let hotelState = useSelector((state) => state.activities, shallowEqual);
    let hotels = hotelState.hotel;
    const [facilitiesforfilter, setFacilitiesforfilter] = useState({});
    const [hotel, setHotel] = useState({});
    const [isLoad, setIsLoad] = useState(false);
    const [facilitieslength, setFacilitieslength] = useState(0);
    const [showMoreFilterCard, setShowMoreFilterCard] = useState(false);
    const classes = useStyles();
    const [isChecked, setIschecked] = useState(false);
    const [facilities, setFacilities] = useState([]);
    const tipodepropy = JSON.parse(localStorage.getItem('tipodepropy'));
    let extras
    if (isLoad) {
        extras = hotel.map((tipo, index) => {
           /* console.log(tipo.caracteristicas);*/
            return (
                <div>
                    <span>{tipo.caracteristicas.nombreCaracteristica}</span>
                    <input
                        key={index}
                        checked={isChecked ? false : null}
                        onChange={handleChange}

                        value={tipo.caracteristicas.caracteristicaId}
                        type="checkbox"
                        onClick={(e) => handleChange(e)}
                    />
                </div>
            );
        })
    }
 
    

    const handleChange = (event) => {
        console.log(event.target.value)
        setIschecked(false);
        if (event.target.checked) {
            
            setFacilities([
                ...facilities,
                 event.target.value,
            ]);
            setFacilitiesforfilter(facilities);
            setFacilitieslength(Object.keys(facilities).length);
        } else {
            for (let key in facilities) {
                if (facilities[key] === event.target.value) {
                    delete facilities[key];
                    facilities.length = facilities.length - 1;
                }
            }
            console.log(facilities);
            setFacilitiesforfilter(facilities);
            setFacilitieslength(Object.keys(facilities).length);
        }
    };
    const handledone = () => {
        
        hotels = dispatch(priceFilter([tipodepropy.tipoPropiedadId],facilities ))

    }
    const handleCheckboxReset = () => {
        setIschecked(!isChecked);
        for (let i in facilities) {
            delete facilities[i];
        }

        setFacilitiesforfilter(facilities);
        setFacilitieslength(Object.keys(facilities).length);
    };
    useEffect(() => {
        console.log(facilities);
        console.log(Object.keys(facilities).length);
        setFacilitieslength(Object.keys(facilities).length);
        setFacilitiesforfilter(facilities);
    }, [facilities]);
  
    if (tipodepropy != null && Object.keys(hotel).length === 0) {

        axios.get("https://propycore.azurewebsites.net/api/TipoPropiedadCaracteristica/ObtenerPorIDdePropiedad/" + tipodepropy.tipoPropiedadId).then(data => { setHotel(data.data); setIsLoad(true); });
    } 
        return (
            <Card className={classes.root} variant="outlined" >
                < CloseIcon onClick={() => {
                    handleMoreFilterCard();
                    console.log('Cerrar ventana');
                }
                } style={{  top: '5px', right: '5px', cursor: 'pointer', color: 'red' }}  />
                <CardContent className={classes.facilitiesCont} >
                    <div>
                        <span>Popular Filters</span>
                    </div>
                    <FacilitiesWrapper>

                        {extras}

                    </FacilitiesWrapper>
                </CardContent>
                <CardContent>
                    <BtnsWrapper>
                        <div>
                            <button onClick={handleCheckboxReset} className="muiBtn1">
                                Reset
                            </button>
                        </div>
                        <div>
                            <button className="muiBtn2" onClick={handledone}>Done</button>
                        </div>
                    </BtnsWrapper>
                </CardContent>
            </Card>
        );
    

}

const HotelstarsWrapper = styled.div`
  display: grid;
  z-index: 3;
  grid-template-columns: 1fr 1fr 1fr 1fr 1fr 5fr;
  width: 100%;
  & > div {
    cursor: pointer;
  }
  & > div:nth-child(1) {
    padding: 1.5rem;
  }

  & > div:nth-child(2) {
    padding: 1.3rem;
  }
  .starBox {
    width: 50px;
    height: 50px;
    border: 1px solid rgb(255, 194, 1);
    padding: 0.8rem;

    img {
      width: 100%;
    }
  }
`;

const FacilitiesWrapper = styled.div`
  display: grid;
  grid-template-columns: 1fr 1fr;
  grid-gap: 2px;
  & > div {
    display: grid;
    grid-template-columns: 1fr 4fr 1fr;
    
    justify-content: start;
    align-items: center;
    box-shadow: 0px 2px 1px -2px grey;
    border-radius: 2px;
    padding: 1rem;
    cursor: pointer;
    input {
      cursor: pointer;
    }
    .MuiSvgIcon-root {
      fill: currentColor;
      width: 1em;
      height: 1em;
      display: inline-block;
      font-size: 2rem;
      transition: fill 200ms cubic-bezier(0.4, 0, 0.2, 1) 0ms;
      flex-shrink: 0;
      user-select: none;
      color: rgb(105, 116, 122);
    }
    :hover {
      background-color: rgb(235, 236, 237);
    }
    img {
      width: 20px;
    }
  }
`;