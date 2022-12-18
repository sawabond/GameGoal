import React, { useContext } from 'react';
import {
  Grid,
  makeStyles,
  Card,
  CardContent,
  CardActions,
  Button,
  CardHeader,
} from '@material-ui/core';
import { Formik, Form, Field } from 'formik';
import * as Yup from 'yup';
import { TextField } from 'formik-material-ui';
import axios from 'axios';
import { userContext } from '../Contexts/userContext';
import { Navigate } from 'react-router';
const useStyle = makeStyles((theme) => ({
  padding: {
    padding: theme.spacing(3),
  },
  button: {
    margin: theme.spacing(1),
  },
}));

const initialValues = {
  username: '',
  password: '',
};

const lowercaseRegEx = /(?=.*[a-z])/;
const uppercaseRegEx = /(?=.*[A-Z])/;
const numericRegEx = /(?=.*[0-9])/;
const lengthRegEx = /(?=.{6,})/;

let validationSchema = Yup.object().shape({
  password: Yup.string()
    .matches(
      lowercaseRegEx,
      'Must contain one lowercase alphabetical character!'
    )
    .matches(
      uppercaseRegEx,
      'Must contain one uppercase alphabetical character!'
    )
    .matches(numericRegEx, 'Must contain one numeric character!')
    .matches(lengthRegEx, 'Must contain 6 characters!')
    .required('Required!'),
});
export default function FormRegistration() {
  const { user, setUser } = useContext(userContext);
  const classes = useStyle();
  const onSubmit = (values, { resetForm }) => {
    axios
      .post('https://localhost:7184/api/User/register-company', {
        headers: {
          'Content-Type': 'application/json',
        },
        username: values.username,
        password: values.password,
      })
      .then((response) => {
        if (response.data.error) {
          console.log(response.data.error);
        } else {
          setUser((prevState) => ({
            ...response.data,
          }));
        }
        resetForm();
      });
  };
  sessionStorage.setItem('user', JSON.stringify(user));
  if (user) {
    return <Navigate replace to={'/'} />;
  }
  return (
    <>
      <Grid
        container
        justifyContent="center"
        style={{ alignItems: 'center', height: '100%' }}
      >
        <Grid item md={6} style={{ margin: '2%' }}>
          <Card className={classes.padding}>
            <CardHeader title="REGISTER FORM"></CardHeader>
            <Formik
              initialValues={initialValues}
              validationSchema={validationSchema}
              onSubmit={onSubmit}
            >
              {({ dirty, isValid, values, handleChange, handleBlur }) => {
                return (
                  <Form>
                    <CardContent>
                      <Grid item container spacing={1} justifyContent="center">
                        <Grid item xs={12} sm={6} md={6}>
                          <Field
                            label="UserName"
                            variant="outlined"
                            fullWidth
                            name="username"
                            value={values.username}
                            component={TextField}
                          />
                        </Grid>
                        <Grid item xs={12} sm={6} md={6}>
                          <Field
                            label="Password"
                            variant="outlined"
                            fullWidth
                            name="password"
                            value={values.password}
                            type="password"
                            component={TextField}
                          />
                        </Grid>
                      </Grid>
                    </CardContent>
                    <CardActions>
                      <Button
                        variant="contained"
                        color="primary"
                        type="Submit"
                        className={classes.button}
                      >
                        REGISTER
                      </Button>
                    </CardActions>
                  </Form>
                );
              }}
            </Formik>
          </Card>
        </Grid>
      </Grid>
    </>
  );
}
