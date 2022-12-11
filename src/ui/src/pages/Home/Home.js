import React, { useContext } from 'react';
import Header from '../../components/Header';
import { AuthContext } from '../../hooks/useAuth';
export default function Home() {
  const { user, setUser } = useContext(AuthContext);
  return (
    <>
      <Header />
    </>
  );
}
