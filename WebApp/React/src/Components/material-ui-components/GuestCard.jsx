import React from "react";
import { makeStyles } from "@material-ui/core/styles";
import Card from "@material-ui/core/Card";
import CardContent from "@material-ui/core/CardContent";
import "./GuestCardAnimate.css";
import styled from "styled-components";
import { shallowEqual, useDispatch, useSelector } from "react-redux";
import { useState, useEffect } from "react";
import { fliterDormitorios } from '../../store/actions';
import axios from "axios";
import CloseIcon from '@material-ui/icons/Close'; // Agregar el icono de cierre

export default function GuestCard({
  setGuestNumber,
    setRoomsNumber,
    handleGuestSelector,
  top,
  right,
  position,
}) {
  const useStyles = makeStyles({
    root: {
          position: position,
          left:"81rem",
      minWidth: "300px",
      maxWidth: "300px",
      maxHeight: "auto",
      boxShadow: "0px 0px 17px -2px black",
      top: top,
      right: right,
      zIndex: "999",
    },
    content: {
      display: "grid",
      gridTemplateColumns: "1fr",
      alignItems: "center",
      gridGap: "1rem",
    },
    content2: {
      display: "grid",
      gridTemplateColumns: "1fr",
      alignItems: "center",
      gridGap: "1rem",
    },
    btnInput: {
      display: "grid",
      gridTemplateColumns: "1fr 1fr 1fr",
    },
    title: {
      fontSize: 14,
    },
    pos: {
      marginBottom: 12,
    },
  });
    // aca usamos la context de react 
    let cont = 0;

   
    const [values, setValues] = useState({
        'Cocheras': 0, 'Dormitorios':0})
  const dispatch = useDispatch();
  const classes = useStyles();
  const [adults, setAdults] = useState(2);
  const [children, setChildren] = useState(0);
    const [roooms, setRoooms] = useState(1);
  const [state, setState] = useState({});
  const [childrenValues, setChildrenValues] = useState([]);
  const [childrenArr, setChildrenArr] = useState([]);
    const [tipoambiente, setTipoambiente] = useState([]);
    const [isLoad, setIsload] = useState(false);
  
    useEffect(() => {

        axios.get("https://propyy.somee.com/api/TipoAmbiente/obtenerTiposAmbientes").then((res) => {

            setTipoambiente(res.data)
            setIsload(true)
        })

    }
        , []);
    const [inputs, setInputs] = useState({});
    const handleChange = (event) => {
        setValues(values => {
            return { ...values, [event.target.name]: event.target.value };
        });
    };
    
    function handle(num) {

        console.log(num);
        /*  myRefs.current[num].value = myRefs.current[num].value + 1; */
        switch (num) {
            case 0:

                handleCocherasAdd;
                break;
            case 1:

                handleChildrenAdd
                break;
            case 2:
                break;
            case 3:
                break;
        };

    };
  const handleAdultsChange = (e) => {
    setAdults(e.target.value);
  };

  const handleAdultsAdd = () => {
    setAdults(Number(adults) + 1);
  };
  const handleAdultsReduce = () => {
    setAdults(Number(adults) - 1);
  };
  const handleChildrenChange = (e) => {
    setChildren(e.target.value);
    setChildrenArr(new Array(children + 1).fill(0));
  };
  const handleChildrenAdd = () => {
    setChildren(Number(children) + 1);
    setChildrenArr(new Array(children + 1).fill(0));
  };
  const handleChildrenReduce = () => {
    setChildren(Number(children) - 1);

    setChildrenArr(new Array(children - 1).fill(0));
  };
    const [rooms, setRooms] = useState(1);
    const handleDormitoriosReduce = (num) => {
        setRooms(Number(rooms) - 1);
    };
    const handleDormitoriosAdd = (num) => {
        setRooms(Number(rooms) + 1);
    };
    const handleDormitoriosChange = (e) => {
        const name = target.name;
         name=e.target.value;
    };
    const handleCocherasChange = (e) => {
    setRooms(e.target.value);
  };
   
    const handleCocherasReduce = () => {
    setRooms(Number(rooms) - 1);
    };
    
  const handleReset = () => {
    setAdults(2);
    setRooms(1);
    setChildren(0);

    setChildrenArr(new Array(0).fill(0));
  };
  const handleChildrenAge = (event) => {
    const name = event.target.name;
    setState({
      ...state,
      [name]: event.target.value,
    });
    console.log(state);
    setChildrenValues([...childrenValues, state]);
  };
    
  
  // console.log(childrenValues)
  // console.log(childrenArr)
// ACA YA HAY QUE HACER CAMBIOS EN EL STORE PARA PODER MODIFICAR LA LISTA
  const handleApply = () => {
      console.log(values);
      dispatch(fliterDormitorios(values))
  };
    let datos

    if (isLoad) {
        datos = tipoambiente.map((tipo, index) => {
          
        return (
                <PeoplesWrapper>
                    <div className="peoplesSpan">
                        <span>{tipo.nombreTipoAmbiente}</span>
                    
                    
                        
                    <input name={tipo.nombreTipoAmbiente} onChange={(e) => handleChange(e)} value={values[tipo.nombreTipoAmbiente]} type="number" style={{ width: "70px", height: "30px" }} min="0" max="9" />
                        <span>o mas</span>
                   
                   
                </div>    
                     
                   
                </PeoplesWrapper>
            );
        })
    }

    return (
        
      

        <Card className={`AnimateRight ${classes.root}`}>
            < CloseIcon onClick={() => {
                handleGuestSelector();
                console.log('Cerrar ventana');
            }
            } style={{ position: 'absolute', top: '5px', right: '5px', cursor: 'pointer', color: 'red' }} />
      <CardContent className={classes.content}>
              {datos}
       
      </CardContent>
      

      <BottomWrapper>
        <CardContent className={classes.content2}>
          <BtnsWrapper children={children}>
            <div>
              <button onClick={handleReset} className="muiBtn1">
                Reset
              </button>
            </div>
            <div>
              <button onClick={handleApply} className="muiBtn2">
                Apply
              </button>
            </div>
          </BtnsWrapper>
        </CardContent>
      </BottomWrapper>
    </Card>
  );
}

const PeoplesWrapper = styled.div`
  display: grid;
  align-items: center;
  grid-template-columns: 2fr 1fr;
  .peoplesSpan {
    span {
      font-size: 13.33px;
      font-weight: 600;
    }
  }
  .btnInput {
    display: grid;
    grid-template-columns: 1fr 1fr 1fr;
    align-items: center;
    grid-gap: 1rem;
    input {
      width: 35px;
      height: 35px;
      border-radius: 7px;
      text-align: center;
      font-size: 14px;
    }
  }
`;
export const BtnsWrapper = styled.div`
  display: grid;
  grid-template-columns: 1fr 1fr;
  & > div:nth-child(1) {
    justify-content: start;
  }
  & > div:nth-child(2) {
    text-align: end;
  }
  .muiBtn1 {
    width: 7rem;
    padding: 0.8rem;
    background-color: white;
    border: 1px solid rgb(10, 17, 33);
    color: rgb(10, 17, 33);
    border-radius: 10px;
    outline: none;
    font-size: 14px;
    font-weight: bold;
    :hover {
      background-color: gray;
      color: white;
      border: none;
    }
  }
  .muiBtn2 {
    width: 7rem;
    padding: 0.8rem;
    background-color: white;
    background-color: #007fad;
    border: 1px solid #007fad;
    border-bottom-color: #005f81;
    color: white;
    border-radius: 10px;
    outline: none;
    font-size: 14px;
    font-weight: bold;
    :hover {
      border: none;
      background-color: #005f81;
      cursor: pointer;
    }
  }
`;
const ChildrenWrapper = styled.div`
  display: ${(props) => (props.children >= 1 ? "block" : "none")};
  display: grid;
  grid-template-columns: 1fr 1fr 1fr 1fr;
  grid-gap: 1rem;
  margin-top: 1rem;
  margin-bottom: 3rem;

  select {
    height: 4rem;
    border-radius: 1rem;
    text-align: center;
  }
`;

const ReqTextWrapper = styled.div`
  text-align: start;
  span {
    font-size: 13px;
    font-weight: bold;
  }
`;
const HotelPlanWrapper = styled.div`
  display: ${(props) => (props.rooms >= 6 ? "block" : "none")};
  margin-top: 1rem;
  margin-bottom: 1rem;
  text-align: center;
  p {
    text-align: center;
    font-size: 13px;
    font-weight: bold;
    color: rgb(0, 127, 173);
    :hover {
      text-decoration: underline;
      cursor: pointer;
    }
  }
`;
const BottomWrapper = styled.div`
  margin-top: ${(props) => (props.children >= 1 ? "" : "-2rem")};
`;
