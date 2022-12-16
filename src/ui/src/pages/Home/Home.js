import React, { useEffect, useContext } from 'react';
import Header from '../../components/Header';
import { ToastContainer, toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import { AuthContext } from '../../hooks/useAuth';
export default function Home() {
  const { user } = useContext(AuthContext);
  useEffect(() => {
    if (user) {
      toast.success('Your are loggin');
    }
  }, []);
  return (
    <>
      <Header />
      <ToastContainer />
    </>
  );
}
