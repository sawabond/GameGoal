import React, { useContext } from 'react';
import { CssVarsProvider } from '@mui/joy/styles';
import Sheet from '@mui/joy/Sheet';
import Typography from '@mui/joy/Typography';
import TextField from '@mui/joy/TextField';
import Button from '@mui/joy/Button';
import Header from '../../components/Header';
import { useFormik } from 'formik';
import { Link } from 'react-router-dom';
import axios from 'axios';
import { AuthContext } from '../../hooks/useAuth';
import { Navigate } from 'react-router-dom';

export default function Login() {
  const { user, setUser } = useContext(AuthContext);
  const formik = useFormik({
    initialValues: {
      UserName: '',
      Password: '',
    },
    onSubmit: (values, { resetForm }) => {
      axios
        .post(
          `https://localhost:7184/api/User/login?UserName=${values.UserName}&Password=${values.Password}`,
          {
            UserName: values.UserName,
            Password: values.Password,
          }
        )
        .then((response) => {
          if (response.data.error) {
            console.log(response.data.error);
          } else {
            setUser((prevState) => ({
              ...response.data,
            }));
          }
        });
    },
  });
  localStorage.setItem('user', JSON.stringify(user));
  if (user) {
    return <Navigate replace to={'/'} />;
  }
  return (
    <>
      <Header />
      <CssVarsProvider>
        <main>
          <Sheet
            sx={{
              maxWidth: 400,
              mx: 'auto', // margin left & right
              my: 4, // margin top & botom
              py: 3, // padding top & bottom
              px: 2, // padding left & right
              display: 'flex',
              justifyContent: 'center',
              flexDirection: 'column',
              gap: 2,
              borderRadius: 'sm',
              boxShadow: 'md',
            }}
            variant="outlined"
          >
            <div>
              <Typography level="h4" component="h1">
                <b>Welcome!</b>
              </Typography>
              <Typography level="body2">Sign in to continue.</Typography>
            </div>
            <form onSubmit={formik.handleSubmit}>
              <TextField
                name="UserName"
                label="Username"
                onChange={formik.handleChange}
                value={formik.values.UserName}
              />
              <TextField
                name="Password"
                type="password"
                placeholder="password"
                label="Password"
                onChange={formik.handleChange}
                value={formik.values.Password}
              />
              <Button
                sx={{
                  mt: 1, // margin top
                }}
                type="submit"
              >
                Log in
              </Button>
              <Typography fontSize="sm" sx={{ alignSelf: 'center' }}>
                Don&apos;t have an account?
                <Link to={'/registr'}>Sign up</Link>
              </Typography>
            </form>
          </Sheet>
        </main>
      </CssVarsProvider>
    </>
  );
}
